<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="CourseList"
    MasterPageFile="~/Site.master" Title="All Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">All Courses</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">

<div class="course-list-page">
  <div class="container">

    <!-- Breadcrumb -->
    <div class="breadcrumb">
        <a href="Default.aspx">Home</a>
        <span class="sep">&#8250;</span>
        <span class="current">Courses</span>
    </div>

    <!-- Page heading -->
    <div class="page-header">
        <h1>All Courses</h1>
        <p>Discover our wide range of courses designed to help you grow.</p>
    </div>

    <!-- Message (enroll feedback etc.) -->
    <asp:Label ID="lblMessage" runat="server" Visible="false" EnableViewState="false" />

    <!-- ===== FILTER & SEARCH BAR ===== -->
    <div class="filter-bar">

        <!-- Search -->
        <div class="filter-group filter-search">
            <label>Search</label>
            <asp:TextBox ID="txtSearch" runat="server"
                CssClass="form-control"
                placeholder="&#128269; Search courses..."
                AutoPostBack="false" />
        </div>

        <!-- Category filter -->
        <div class="filter-group">
            <label>Category</label>
            <asp:DropDownList ID="ddlCategory" runat="server"
                CssClass="form-control"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                <asp:ListItem Value="">All Categories</asp:ListItem>
                <asp:ListItem Value="Programming">Programming</asp:ListItem>
                <asp:ListItem Value="Web Development">Web Development</asp:ListItem>
                <asp:ListItem Value="Computer Science">Computer Science</asp:ListItem>
                <asp:ListItem Value="Mathematics">Mathematics</asp:ListItem>
                <asp:ListItem Value="Data Science">Data Science</asp:ListItem>
                <asp:ListItem Value="Design">Design</asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Sort -->
        <div class="filter-group">
            <label>Sort By</label>
            <asp:DropDownList ID="ddlSort" runat="server"
                CssClass="form-control"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                <asp:ListItem Value="name_asc">Name (A-Z)</asp:ListItem>
                <asp:ListItem Value="name_desc">Name (Z-A)</asp:ListItem>
                <asp:ListItem Value="newest">Newest First</asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Search button -->
        <asp:Button ID="btnSearch" runat="server"
            Text="Search"
            OnClick="btnSearch_Click"
            CssClass="btn btn-primary" />
    </div>

    <!-- ===== COURSE CARDS GRID ===== -->
    <asp:Repeater ID="rptCourses" runat="server">
        <HeaderTemplate>
            <div class="courses-grid">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="course-card">
                <div class='course-thumbnail <%# GetThumbClass(Eval("category").ToString()) %>'>
                    <span><%# GetCatIcon(Eval("category").ToString()) %></span>
                </div>
                <div class="course-card-body">
                    <span class='course-tag <%# GetTagClass(Eval("category").ToString()) %>'>
                        <%# Eval("category") %>
                    </span>
                    <h3><%# Eval("course_name") %></h3>
                    <p><%# Server.HtmlEncode(Eval("description").ToString().Length > 100
                            ? Eval("description").ToString().Substring(0, 100) + "..."
                            : Eval("description").ToString()) %></p>
                    <div class="course-meta">
                        <span>&#128218; <%# Eval("lesson_count") %> Lessons</span>
                        <span>&#128394; <%# Eval("quiz_count") %> Quizzes</span>
                    </div>
                </div>
                <div class="course-card-footer">
                    <!-- Enroll or Continue button based on enrollment status -->
                    <%# GetActionButton(Eval("course_id"), Eval("is_enrolled")) %>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <!-- No results message -->
    <asp:Panel ID="pnlNoResults" runat="server" Visible="false">
        <div class="empty-state">
            <div class="empty-icon">&#128214;</div>
            <h3>No courses found</h3>
            <p>Try adjusting your search or filters.</p>
        </div>
    </asp:Panel>

    <!-- ===== PAGINATION ===== -->
    <asp:Panel ID="pnlPagination" runat="server">
        <div class="pagination">
            <asp:Button ID="btnPrev" runat="server"
                Text="&laquo; Previous"
                OnClick="btnPrev_Click"
                CssClass="page-btn" />

            <asp:Repeater ID="rptPages" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lbPage" runat="server"
                        Text='<%# Eval("PageNum") %>'
                        CommandArgument='<%# Eval("PageNum") %>'
                        OnCommand="lbPage_Command"
                        CssClass='<%# (int)Eval("PageNum") == CurrentPage ? "page-btn active" : "page-btn" %>' />
                </ItemTemplate>
            </asp:Repeater>

            <asp:Button ID="btnNext" runat="server"
                Text="Next &raquo;"
                OnClick="btnNext_Click"
                CssClass="page-btn" />
        </div>
    </asp:Panel>

    <!-- Enroll hidden form (for POST action) -->
    <asp:HiddenField ID="hdnEnrollCourseId" runat="server" />
    <asp:Button ID="btnEnroll" runat="server" Style="display:none;"
        OnClick="btnEnroll_Click" />

  </div><!-- /container -->
</div>

</asp:Content>
