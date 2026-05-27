<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminManageCourses.aspx.cs" Inherits="AdminManageCourses"
    MasterPageFile="~/AdminSite.master" Title="Manage Courses" %>
<%--
    Author:      Ng Ern Chi
    Description: Course management page (ASPX markup)
    Date:        23/5/2026
--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTitle" runat="server">
    Manage Courses
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">

<div class="admin-page">
  <div class="container">

    <div class="admin-page-header">
        <h1>Manage Courses</h1>
        <p>Add, edit and remove courses from the platform.</p>
    </div>

    <!-- Stats Bar -->
    <div class="admin-stats-bar">
        <div class="admin-stat-chip">
            <div class="asc-icon" style="background:#D1FAE5; color:#065F46; font-size:1.1rem;">&#128218;</div>
            <div class="asc-info">
                <div class="asc-num"><asp:Literal ID="litCountCourses" runat="server">0</asp:Literal></div>
                <div class="asc-lbl">Total Courses</div>
            </div>
        </div>
        <div class="admin-stat-chip">
            <div class="asc-icon" style="background:#EDE9FE; color:#6D28D9; font-size:1.1rem;">&#128214;</div>
            <div class="asc-info">
                <div class="asc-num"><asp:Literal ID="litCountLessons" runat="server">0</asp:Literal></div>
                <div class="asc-lbl">Total Lessons</div>
            </div>
        </div>
        <div class="admin-stat-chip">
            <div class="asc-icon" style="background:#EDE9FE; color:#5B21B6; font-size:1.1rem;">&#128200;</div>
            <div class="asc-info">
                <div class="asc-num"><asp:Literal ID="litCountEnrolled" runat="server">0</asp:Literal></div>
                <div class="asc-lbl">Total Enrollments</div>
            </div>
        </div>
    </div>

    <!-- Messages -->
    <asp:Label ID="lblMessage" runat="server" Visible="false"
        style="display:block; margin-bottom:16px;" />

    <!-- Search + Add Bar -->
    <div class="action-bar">
        <div style="display:flex; gap:10px; flex-wrap:wrap; align-items:center;">
            <asp:DropDownList ID="ddlCategoryFilter" runat="server" CssClass="form-control" style="width:180px;"
                AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryFilter_Changed">
                <asp:ListItem Value="">All Categories</asp:ListItem>
                <asp:ListItem Value="Programming">Programming</asp:ListItem>
                <asp:ListItem Value="Web Development">Web Development</asp:ListItem>
                <asp:ListItem Value="Computer Science">Computer Science</asp:ListItem>
                <asp:ListItem Value="Mathematics">Mathematics</asp:ListItem>
                <asp:ListItem Value="Data Science">Data Science</asp:ListItem>
                <asp:ListItem Value="Design">Design</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                placeholder="Search courses..." style="width:260px;" />
            <asp:Button ID="btnSearch" runat="server" Text="Search"
                OnClick="btnSearch_Click" CssClass="btn btn-primary btn-sm" />
            <asp:Button ID="btnClear" runat="server" Text="Clear"
                OnClick="btnClear_Click" CssClass="btn btn-outline btn-sm" />
        </div>
        <asp:Button ID="btnShowAdd" runat="server" Text="&#43; Add New Course"
            OnClick="btnShowAdd_Click" CssClass="btn btn-primary" />
    </div>

    <!-- Add Course Form -->
    <asp:Panel ID="pnlAddCourse" runat="server" Visible="false" CssClass="details-insert-card">
        <h3>Add New Course</h3>
        <div class="form-row">
            <div class="form-group">
                <label>Course Name *</label>
                <asp:TextBox ID="txtAddName" runat="server" CssClass="form-control"
                    placeholder="e.g. Introduction to Python" MaxLength="200" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddName"
                    ValidationGroup="AddCourse" CssClass="field-validator"
                    ErrorMessage="Course name is required." Display="Dynamic">Course name is required.</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label>Category *</label>
                <asp:DropDownList ID="ddlAddCategory" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Select Category --</asp:ListItem>
                    <asp:ListItem Value="Programming">Programming</asp:ListItem>
                    <asp:ListItem Value="Web Development">Web Development</asp:ListItem>
                    <asp:ListItem Value="Computer Science">Computer Science</asp:ListItem>
                    <asp:ListItem Value="Mathematics">Mathematics</asp:ListItem>
                    <asp:ListItem Value="Data Science">Data Science</asp:ListItem>
                    <asp:ListItem Value="Design">Design</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAddCategory"
                    ValidationGroup="AddCourse" CssClass="field-validator"
                    InitialValue="" ErrorMessage="Please select a category." Display="Dynamic">Please select a category.</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label>Description *</label>
            <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control"
                TextMode="MultiLine" Rows="4"
                placeholder="Describe what students will learn in this course..." MaxLength="1000" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddDescription"
                ValidationGroup="AddCourse" CssClass="field-validator"
                ErrorMessage="Description is required." Display="Dynamic">Description is required.</asp:RequiredFieldValidator>
        </div>
        <asp:ValidationSummary runat="server" ValidationGroup="AddCourse"
            CssClass="validation-summary" HeaderText="Please fix the following errors:" />
        <div style="display:flex; gap:10px; margin-top:8px;">
            <asp:Button ID="btnAddCourse" runat="server" Text="Add Course"
                OnClick="btnAddCourse_Click" CssClass="btn btn-primary" ValidationGroup="AddCourse" />
            <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel"
                OnClick="btnCancelAdd_Click" CssClass="btn btn-outline" CausesValidation="false" />
        </div>
    </asp:Panel>

    <!-- Edit Course Form -->
    <asp:Panel ID="pnlEditCourse" runat="server" Visible="false" CssClass="details-insert-card">
        <h3>Edit Course</h3>
        <asp:HiddenField ID="hdnEditCourseId" runat="server" />
        <div class="form-row">
            <div class="form-group">
                <label>Course Name *</label>
                <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control" MaxLength="200" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEditName"
                    ValidationGroup="EditCourse" CssClass="field-validator"
                    ErrorMessage="Course name is required." Display="Dynamic">Course name is required.</asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label>Category *</label>
                <asp:DropDownList ID="ddlEditCategory" runat="server" CssClass="form-control">
                    <asp:ListItem Value="Programming">Programming</asp:ListItem>
                    <asp:ListItem Value="Web Development">Web Development</asp:ListItem>
                    <asp:ListItem Value="Computer Science">Computer Science</asp:ListItem>
                    <asp:ListItem Value="Mathematics">Mathematics</asp:ListItem>
                    <asp:ListItem Value="Data Science">Data Science</asp:ListItem>
                    <asp:ListItem Value="Design">Design</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label>Description *</label>
            <asp:TextBox ID="txtEditDescription" runat="server" CssClass="form-control"
                TextMode="MultiLine" Rows="4" MaxLength="1000" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEditDescription"
                ValidationGroup="EditCourse" CssClass="field-validator"
                ErrorMessage="Description is required." Display="Dynamic">Description is required.</asp:RequiredFieldValidator>
        </div>
        <asp:ValidationSummary runat="server" ValidationGroup="EditCourse"
            CssClass="validation-summary" HeaderText="Please fix the following errors:" />
        <div style="display:flex; gap:10px; margin-top:8px;">
            <asp:Button ID="btnSaveEdit" runat="server" Text="Save Changes"
                OnClick="btnSaveEdit_Click" CssClass="btn btn-primary" ValidationGroup="EditCourse" />
            <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel"
                OnClick="btnCancelEdit_Click" CssClass="btn btn-outline" CausesValidation="false" />
        </div>
    </asp:Panel>

    <!-- Courses Grid -->
    <div class="gridview-wrapper">
        <asp:GridView ID="gvCourses" runat="server"
            CssClass="gridview-table"
            AutoGenerateColumns="False"
            DataKeyNames="course_id"
            AllowPaging="True"
            PageSize="10"
            OnPageIndexChanging="gvCourses_PageIndexChanging"
            OnRowCommand="gvCourses_RowCommand"
            EmptyDataText="No courses found.">
            <Columns>
                <asp:BoundField DataField="course_id"   HeaderText="ID"       ItemStyle-Width="50px" />
                <asp:BoundField DataField="course_name" HeaderText="Course Name" />
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <span class='course-tag <%# GetTagClass(Eval("category").ToString()) %>'>
                            <%# Server.HtmlEncode(Eval("category").ToString()) %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="lesson_count" HeaderText="Lessons"  ItemStyle-Width="80px" />
                <asp:BoundField DataField="quiz_count"   HeaderText="Quizzes"  ItemStyle-Width="80px" />
                <asp:BoundField DataField="enrolled"     HeaderText="Enrolled" ItemStyle-Width="80px" />
                <asp:TemplateField HeaderText="Actions" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <div class="table-actions">
                            <asp:LinkButton ID="lbEdit" runat="server"
                                CommandName="EditCourse"
                                CommandArgument='<%# Eval("course_id") %>'
                                CssClass="btn btn-outline btn-sm">
                                <span class="btn-icon">&#9998;</span> Edit
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbDelete" runat="server"
                                CommandName="DeleteCourse"
                                CommandArgument='<%# Eval("course_id") %>'
                                CssClass="btn btn-danger btn-sm"
                                OnClientClick="return confirm('Delete this course? All lessons, quizzes, and enrollment data will also be removed.');">
                                <span class="btn-icon">&#128465;</span> Delete
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="gridview-pager" />
        </asp:GridView>
    </div>

  </div>
</div>

</asp:Content>
