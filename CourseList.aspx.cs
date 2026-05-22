using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CourseList : Page
{
    private const int PageSize = 6;  // courses per page
    public int CurrentPage { get; private set; }
    private int totalPages = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentPage = 1;
            ViewState["CurrentPage"] = 1;
            LoadCourses();
        }
        else
        {
            CurrentPage = ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 1;
        }
    }

    private void LoadCourses()
    {
        string search   = txtSearch.Text.Trim();
        string category = ddlCategory.SelectedValue;
        string sort     = ddlSort.SelectedValue;
        int    userId   = Session["UserId"] != null ? int.Parse(Session["UserId"].ToString()) : 0;

        string connStr = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Build dynamic SQL with parameterized search
            string whereClause = " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(search))
                whereClause += " AND (c.course_name LIKE @search OR c.description LIKE @search OR c.category LIKE @search) ";
            if (!string.IsNullOrEmpty(category))
                whereClause += " AND c.category = @category ";

            string orderClause = sort == "name_desc" ? " ORDER BY c.course_name DESC " :
                                 sort == "newest"    ? " ORDER BY c.course_id DESC " :
                                                       " ORDER BY c.course_name ASC ";

            // Count total records for pagination
            string countSql = "SELECT COUNT(*) FROM Courses c" + whereClause;
            SqlCommand countCmd = new SqlCommand(countSql, conn);
            if (!string.IsNullOrEmpty(search))   countCmd.Parameters.AddWithValue("@search",   "%" + search + "%");
            if (!string.IsNullOrEmpty(category)) countCmd.Parameters.AddWithValue("@category", category);

            int totalRecords = (int)countCmd.ExecuteScalar();
            totalPages = Math.Max(1, (int)Math.Ceiling((double)totalRecords / PageSize));

            // Clamp current page
            if (CurrentPage > totalPages) CurrentPage = totalPages;
            int offset = (CurrentPage - 1) * PageSize;

            // Fetch page of courses
            string sql = @"
                SELECT
                    c.course_id,
                    c.course_name,
                    c.description,
                    c.category,
                    (SELECT COUNT(*) FROM Lessons l WHERE l.course_id = c.course_id) AS lesson_count,
                    (SELECT COUNT(*) FROM Quizzes q WHERE q.course_id = c.course_id) AS quiz_count,
                    CASE WHEN e.enrollment_id IS NOT NULL THEN 1 ELSE 0 END AS is_enrolled
                FROM Courses c
                LEFT JOIN Enrollment e ON c.course_id = e.course_id AND e.user_id = @userId
                " + whereClause + orderClause +
                " OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId",   userId);
            cmd.Parameters.AddWithValue("@offset",   offset);
            cmd.Parameters.AddWithValue("@pageSize", PageSize);
            if (!string.IsNullOrEmpty(search))   cmd.Parameters.AddWithValue("@search",   "%" + search + "%");
            if (!string.IsNullOrEmpty(category)) cmd.Parameters.AddWithValue("@category", category);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                rptCourses.Visible   = true;
                rptCourses.DataSource = dt;
                rptCourses.DataBind();
                pnlNoResults.Visible = false;
            }
            else
            {
                rptCourses.Visible   = false;
                pnlNoResults.Visible = true;
            }

            BuildPagination(totalPages);
        }
    }

    // Build pagination number buttons
    private void BuildPagination(int total)
    {
        if (total <= 1)
        {
            pnlPagination.Visible = false;
            return;
        }

        pnlPagination.Visible = true;
        btnPrev.Enabled = CurrentPage > 1;
        btnNext.Enabled = CurrentPage < total;

        var pages = new System.Collections.Generic.List<object>();
        for (int i = 1; i <= total; i++)
            pages.Add(new { PageNum = i });

        rptPages.DataSource = pages;
        rptPages.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrentPage = 1;
        ViewState["CurrentPage"] = 1;
        LoadCourses();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPage = 1;
        ViewState["CurrentPage"] = 1;
        LoadCourses();
    }

    protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCourses();
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            ViewState["CurrentPage"] = CurrentPage;
            LoadCourses();
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        CurrentPage++;
        ViewState["CurrentPage"] = CurrentPage;
        LoadCourses();
    }

    protected void lbPage_Command(object sender, CommandEventArgs e)
    {
        CurrentPage = int.Parse(e.CommandArgument.ToString());
        ViewState["CurrentPage"] = CurrentPage;
        LoadCourses();
    }

    // Enroll button handler — called via JavaScript
    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        int courseId = 0;
        if (!int.TryParse(hdnEnrollCourseId.Value, out courseId)) return;

        int userId = int.Parse(Session["UserId"].ToString());

        string connStr = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Check if already enrolled
            SqlCommand checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM Enrollment WHERE user_id=@uid AND course_id=@cid", conn);
            checkCmd.Parameters.AddWithValue("@uid", userId);
            checkCmd.Parameters.AddWithValue("@cid", courseId);

            if ((int)checkCmd.ExecuteScalar() > 0)
            {
                // Already enrolled — go to lesson
                Response.Redirect("Lesson.aspx?courseId=" + courseId);
                return;
            }

            // Insert enrollment record
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Enrollment (user_id, course_id) VALUES (@uid, @cid)", conn);
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@cid", courseId);
            cmd.ExecuteNonQuery();
        }

        // Redirect directly to the lesson page after successful enrollment
        Response.Redirect("Lesson.aspx?courseId=" + courseId);
    }

    // Returns correct action button HTML based on enrollment and login state
    protected string GetActionButton(object courseId, object isEnrolled)
    {
        int cid = Convert.ToInt32(courseId);

        if (Session["UserId"] == null)
        {
            // Not logged in — show enroll that redirects to login
            return string.Format(
                "<a href=\"Login.aspx\" class=\"btn btn-outline btn-sm btn-block\">Enroll Now</a>");
        }

        bool enrolled = Convert.ToInt32(isEnrolled) == 1;

        if (enrolled)
        {
            return string.Format(
                "<a href=\"Lesson.aspx?courseId={0}\" class=\"btn btn-primary btn-sm btn-block\">Continue Learning</a>", cid);
        }
        else
        {
            return string.Format(
                "<a href=\"CourseList.aspx?enroll={0}\" class=\"btn btn-outline btn-sm btn-block\" " +
                "onclick=\"document.getElementById('{1}').value='{0}'; document.getElementById('{2}').click(); return false;\">Enroll Now</a>",
                cid,
                hdnEnrollCourseId.ClientID,
                btnEnroll.ClientID);
        }
    }

    // CSS helper methods
    protected string GetThumbClass(string cat)
    {
        switch (cat.ToLower())
        {
            case "programming":      return "thumb-programming";
            case "web development":  return "thumb-webdev";
            case "computer science": return "thumb-cs";
            case "mathematics":      return "thumb-math";
            case "data science":     return "thumb-datascience";
            case "design":           return "thumb-design";
            default:                 return "thumb-default";
        }
    }

    protected string GetTagClass(string cat)
    {
        switch (cat.ToLower())
        {
            case "programming":      return "tag-programming";
            case "web development":  return "tag-webdev";
            case "computer science": return "tag-cs";
            case "mathematics":      return "tag-math";
            case "data science":     return "tag-datascience";
            case "design":           return "tag-design";
            default:                 return "tag-default";
        }
    }

    protected string GetCatIcon(string cat)
    {
        switch (cat.ToLower())
        {
            case "programming":      return "&#128187;";
            case "web development":  return "&#127760;";
            case "computer science": return "&#128296;";
            case "mathematics":      return "&#8734;";
            case "data science":     return "&#128202;";
            case "design":           return "&#127912;";
            default:                 return "&#128214;";
        }
    }
}
