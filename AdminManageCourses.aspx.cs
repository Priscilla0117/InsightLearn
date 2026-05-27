/*
 * Author:      Ng Ern Chi
 * Description: Course management page (code-behind)
 * Date:        23/5/2026
 */
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminManageCourses : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCourses();
        }
    }

    private void LoadCourses()
    {
        string search   = txtSearch.Text.Trim();
        string category = ddlCategoryFilter.SelectedValue;
        string connStr  = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sql = @"
                SELECT
                    c.course_id,
                    c.course_name,
                    c.category,
                    (SELECT COUNT(*) FROM Lessons  l WHERE l.course_id = c.course_id) AS lesson_count,
                    (SELECT COUNT(*) FROM Quizzes  q WHERE q.course_id = c.course_id) AS quiz_count,
                    (SELECT COUNT(*) FROM Enrollment e WHERE e.course_id = c.course_id) AS enrolled
                FROM Courses c
                WHERE 1=1 ";

            if (!string.IsNullOrEmpty(search))
                sql += " AND (c.course_name LIKE @search OR c.description LIKE @search) ";
            if (!string.IsNullOrEmpty(category))
                sql += " AND c.category = @category ";

            sql += " ORDER BY c.course_id";

            SqlCommand cmd = new SqlCommand(sql, conn);

            if (!string.IsNullOrEmpty(search))
                cmd.Parameters.AddWithValue("@search", "%" + search + "%");
            if (!string.IsNullOrEmpty(category))
                cmd.Parameters.AddWithValue("@category", category);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvCourses.DataSource = dt;
            gvCourses.DataBind();
        }

        LoadSummaryStats();
    }

    private void LoadSummaryStats()
    {
        string connStr = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Courses", conn);
            litCountCourses.Text = cmd.ExecuteScalar().ToString();

            cmd = new SqlCommand("SELECT COUNT(*) FROM Lessons", conn);
            litCountLessons.Text = cmd.ExecuteScalar().ToString();

            cmd = new SqlCommand("SELECT COUNT(*) FROM Enrollment", conn);
            litCountEnrolled.Text = cmd.ExecuteScalar().ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvCourses.PageIndex = 0;
        LoadCourses();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        ddlCategoryFilter.SelectedIndex = 0;
        gvCourses.PageIndex = 0;
        LoadCourses();
    }

    protected void ddlCategoryFilter_Changed(object sender, EventArgs e)
    {
        gvCourses.PageIndex = 0;
        LoadCourses();
    }

    protected void gvCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCourses.PageIndex = e.NewPageIndex;
        LoadCourses();
    }

    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        pnlAddCourse.Visible  = true;
        pnlEditCourse.Visible = false;
        txtAddName.Text        = "";
        txtAddDescription.Text = "";
        ddlAddCategory.SelectedIndex = 0;
    }

    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {
        pnlAddCourse.Visible = false;
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        string name     = txtAddName.Text.Trim();
        string desc     = txtAddDescription.Text.Trim();
        string category = ddlAddCategory.SelectedValue;

        string connStr = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Courses (course_name, description, category) VALUES (@name, @desc, @cat)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@cat",  category);
            cmd.ExecuteNonQuery();
        }

        pnlAddCourse.Visible = false;
        ShowMessage("&#10003; Course added successfully!", true);
        LoadCourses();
    }

    protected void gvCourses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int courseId = int.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "EditCourse")
        {
            LoadCourseForEdit(courseId);
        }
        else if (e.CommandName == "DeleteCourse")
        {
            DeleteCourse(courseId);
        }
    }

    private void LoadCourseForEdit(int courseId)
    {
        string connStr = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT course_id, course_name, description, category FROM Courses WHERE course_id = @cid", conn);
            cmd.Parameters.AddWithValue("@cid", courseId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                hdnEditCourseId.Value    = reader["course_id"].ToString();
                txtEditName.Text         = reader["course_name"].ToString();
                txtEditDescription.Text  = reader["description"].ToString();
                ddlEditCategory.SelectedValue = reader["category"].ToString();
            }
        }

        pnlEditCourse.Visible = true;
        pnlAddCourse.Visible  = false;
    }

    protected void btnSaveEdit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        int courseId    = int.Parse(hdnEditCourseId.Value);
        string name     = txtEditName.Text.Trim();
        string desc     = txtEditDescription.Text.Trim();
        string category = ddlEditCategory.SelectedValue;

        string connStr = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "UPDATE Courses SET course_name=@name, description=@desc, category=@cat WHERE course_id=@cid", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@cat",  category);
            cmd.Parameters.AddWithValue("@cid",  courseId);
            cmd.ExecuteNonQuery();
        }

        pnlEditCourse.Visible = false;
        ShowMessage("&#10003; Course updated successfully!", true);
        LoadCourses();
    }

    protected void btnCancelEdit_Click(object sender, EventArgs e)
    {
        pnlEditCourse.Visible = false;
    }

    private void DeleteCourse(int courseId)
    {
        string connStr = ConfigurationManager.ConnectionStrings["InsightLearnDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            // FK cascade deletes lessons, quizzes, enrollments
            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Courses WHERE course_id = @cid", conn);
            cmd.Parameters.AddWithValue("@cid", courseId);
            cmd.ExecuteNonQuery();
        }

        ShowMessage("&#10003; Course deleted successfully.", true);
        LoadCourses();
    }

    // Returns CSS tag class for category
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

    private void ShowMessage(string msg, bool success)
    {
        lblMessage.Text     = msg;
        lblMessage.CssClass = success ? "alert alert-success" : "alert alert-danger";
        lblMessage.Visible  = true;
    }
}
