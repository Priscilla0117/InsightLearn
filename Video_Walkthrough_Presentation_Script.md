# InsightLearn Video Walkthrough Presentation Script

Recommended video length: 10 to 13 minutes  
Purpose: Show registration, login, and the special features developed by each member.  
Tone: Natural, clear, and fast enough because the detailed explanation will be done during the physical presentation.

## Before Recording

Prepare these accounts and data before starting:

- Admin account: `admin@insightlearn.com`
- Student account: either use an existing student or register a new one during the demo.
- Keep one test course, lesson, quiz, and question ready so the demo does not take too long.
- Each member should speak only for their own section.
- Show at least one validation example and one database action such as add, edit, or delete.

## Opening

Speaker: Group representative

Script:

Hello everyone. This is our website walkthrough for InsightLearn, a web-based learning system developed using ASP.NET Web Forms, SQL Server LocalDB, ADO.NET, HTML, CSS, and JavaScript.

InsightLearn supports three main types of users: public visitors, registered students, and administrators. In this video, we will show how users can register, log in, browse courses, learn through lessons, take quizzes, track progress, and view dashboard information.

Each member will demonstrate the module they developed, so we can show how the system meets the implementation criteria, including layout, authentication, dynamic content, CRUD operations, validation, navigation, and usability.

First, we will start with user access.

## 1. User Access Engineer - Ian Lim

Main files:

- `Login.aspx`
- `Login.aspx.cs`
- `Register.aspx`
- `Register.aspx.cs`
- `Settings.aspx`
- `Settings.aspx.cs`
- `AdminManageUsers.aspx`
- `AdminManageUsers.aspx.cs`
- `Site.master`
- `Site.master.cs`
- `AdminSite.master`
- `AdminSite.master.cs`
- `Web.config`
- `Styles/layout.css`

Demo actions:

1. Open the home page.
2. Go to Register.
3. Submit an empty form once to show validation.
4. Register a new student account.
5. Log in as the student.
6. Show that the student navigation is different.
7. Log out.
8. Log in as admin.
9. Show the admin navigation.
10. Open Manage Users.
11. Add or edit a test user.

Script:

Hi, I am Ian, and I worked on the User Access module.

This part of the system handles registration, login, logout, profile settings, role-based navigation, and admin user management.

I will start from the registration page. If I try to submit the form without entering the required details, the system shows validation messages. This helps prevent incomplete user records from being inserted into the database.

Now I will register a new student account. After the account is created, the user can log in using the email and password.

Once the student logs in, the navigation changes. The student can access student features such as the dashboard, courses, lessons, quizzes, and certificates.

Next, I will log out and log in as an admin. After admin login, the system redirects to the admin dashboard. The admin navigation is different because administrators can manage website content and database records.

Here, in Manage Users, the admin can view users, search users, add new users, edit user details, and delete users. This demonstrates CRUD operations for user records.

So, this module covers authentication, authorization, form validation, session-based navigation, and user database management.

Transition:

Next, we will demonstrate the Course module.

## 2. Course Module Engineer - Ng Ern Chi

Main files:

- `AdminManageCourses.aspx`
- `AdminManageCourses.aspx.cs`
- `CourseList.aspx`
- `CourseList.aspx.cs`
- `App_Code/DatabaseHelper.cs`
- `Styles/components.css`
- `Default.aspx`
- `Default.aspx.cs`

Demo actions:

1. Stay logged in as admin.
2. Open Manage Courses.
3. Show course list, search, and category filter.
4. Try adding a course without required fields to show validation.
5. Add a test course.
6. Edit the test course.
7. Show publish or unpublish.
8. Explain unpublished courses are hidden from students.
9. Open public/student Course List.
10. Enroll in a published course.

Script:

Hi, I am Ng Ern Chi, and I worked on the Course module.

This module controls the full course lifecycle. Admins can create, update, delete, search, filter, and publish courses. Students can browse published courses and enroll in them.

On the admin course page, the course records are loaded dynamically from the database. The table shows the course name, category, number of lessons, number of quizzes, enrollment count, and publish status.

I can filter courses by category and search by course name or description. This makes the page easier to use when there are many courses.

Now I will add a new course. If I submit the form without the required information, the system shows validation messages. After entering the course name, category, and description, the record is inserted into the database.

I can also edit the course details. For example, I can update the course name or description, and the changes are saved back to the database.

The special feature in this module is the publish toggle. Admins can decide whether a course is visible to students. Unpublished courses are hidden from the student course list. This is important because students should only see courses that are ready.

Now on the course list page, students can browse available courses, search, filter by category, sort the list, and enroll in a course.

This module covers course layout, dynamic database content, course CRUD, publishing validation, student enrollment, and navigation between public and student pages.

Transition:

Next, we will show the learning progress and lesson module.

## 3. Learning Progress Engineer - Foo Kim Chean

Main files:

- `AdminManageLessons.aspx`
- `AdminManageLessons.aspx.cs`
- `Lesson.aspx`
- `Lesson.aspx.cs`
- `Certificate.aspx`
- `Certificate.aspx.cs`
- `Styles/student-pages.css`

Demo actions:

1. Log in as admin.
2. Open Manage Lessons.
3. Show lesson list and course filter.
4. Add or edit a lesson.
5. Show validation for required lesson fields.
6. Log in as student.
7. Open a course lesson.
8. Show video area and lesson content.
9. Use next and previous lesson navigation.
10. Click Mark as Complete.
11. Show progress percentage update.
12. Open Certificates page.

Script:

Hi, I am Foo Kim Chean, and I worked on the Learning Progress module.

This module includes lesson management, lesson viewing, progress tracking, and certificates.

From the admin side, lessons can be managed through the Manage Lessons page. The admin can select a course, search lessons, add a new lesson, edit lesson content, and delete lessons.

When adding a lesson, the system validates important fields such as course, lesson title, and lesson content. This prevents incomplete lesson records from being saved.

Now I will switch to the student side. After a student enrolls in a course, they can open the lesson page. The lesson content is loaded from the database, and the page also supports video content, which demonstrates multimedia usage.

The student can move between lessons using the previous and next buttons. The sidebar also shows all lessons in the course.

When the student clicks Mark as Complete, the system saves the progress into the database and updates the progress percentage. This allows the student dashboard and certificate page to know which lessons have been completed.

After completing all lessons in a course, the student can view the certificate page. The certificate is generated based on the completed course data.

So this module demonstrates lesson CRUD, multimedia learning content, dynamic progress tracking, student navigation, and certificate display.

Transition:

Next, we will demonstrate the assessment module.

## 4. Assessment Module Engineer - Chan Kar Jun

Main files:

- `AdminManageQuizzes.aspx`
- `AdminManageQuizzes.aspx.cs`
- `Quiz.aspx`
- `Quiz.aspx.cs`
- `QuizResult.aspx`
- `QuizResult.aspx.cs`
- `Scripts/scripts.js`

Demo actions:

1. Log in as admin.
2. Open Manage Quizzes.
3. Add or edit a quiz.
4. Show validation for quiz title/course.
5. Log in as student.
6. Open a quiz from a course.
7. Answer questions.
8. Use next and previous buttons.
9. Flag one question.
10. Submit quiz.
11. Show quiz result page and answer review.

Script:

Hi, I am Chan Kar Jun, and I worked on the Assessment module.

This module manages quizzes and the student quiz-taking flow.

On the admin side, quizzes can be created, edited, deleted, and linked to courses. The quiz list is loaded dynamically from the database, and the admin can see how many questions each quiz has.

The form also includes validation, so a quiz cannot be created without a title and course.

Now I will show the student quiz page. When a student opens a quiz, the questions are loaded from the database. The student can select an answer, go to the next or previous question, and flag questions for review.

The JavaScript helps with the quiz navigation and selected answer display, while the server-side code stores the quiz state during the session.

After the student submits the quiz, the system calculates the score, saves the quiz attempt into the database, and redirects to the result page.

On the result page, the student can see the final score, number of correct and wrong answers, pass or fail status, and a review of the selected answers.

This module demonstrates dynamic assessment content, student interaction, database insertion of quiz attempts, scoring logic, and a complete assessment workflow.

Transition:

Finally, we will show the analytics, question bank, and database-related features.

## 5. Analytics Module Engineer - Oswald Loh Kar Tzun

Main files:

- `AdminManageQuestions.aspx`
- `AdminManageQuestions.aspx.cs`
- `AdminDashboard.aspx`
- `AdminDashboard.aspx.cs`
- `StudentDashboard.aspx`
- `StudentDashboard.aspx.cs`
- `database.sql`
- `Styles/admin-pages.css`
- `Styles/base.css`
- `About.aspx`
- `About.aspx.cs`
- `Images`

Demo actions:

1. Log in as admin.
2. Open Admin Dashboard.
3. Show total students, courses, quizzes, enrollments, lessons, and quiz attempts.
4. Show recent enrollments and top courses.
5. Open Manage Questions through a quiz.
6. Add, edit, or delete a test question.
7. Log in as student.
8. Open Student Dashboard.
9. Show enrolled courses, recent quiz results, performance chart, achievements, and recommendations.
10. Open About page and show team roles.

Script:

Hi, I am Oswald, and I worked on the Analytics module, question bank, and database structure.

First, this is the admin dashboard. The dashboard displays live statistics from the database, such as total students, courses, quizzes, enrollments, lessons, quiz attempts, and average score.

It also shows recent enrollments and top courses. These values are not hard-coded. They are retrieved using SQL queries and displayed dynamically.

Next, I will show the question bank. From the quiz management page, the admin can open the questions for a selected quiz. Here, the admin can add, edit, and delete quiz questions.

Each question is linked to a quiz and contains four options with one correct answer. This part demonstrates CRUD operations for question records.

Now I will show the student dashboard. The student dashboard displays enrolled courses, progress, recent quiz results, performance chart, achievements, and recommendations. These sections are generated based on the student's actual activity in the database.

The database script defines the main tables used by the whole system, including Users, Courses, Lessons, Enrollment, Lesson Progress, Quizzes, Questions, Quiz Attempts, and Attempt Answers.

Lastly, the About page shows the team members and their roles, so the system clearly presents each member's contribution.

This module demonstrates dashboard analytics, question CRUD, database relationships, dynamic content, and shared styling.

## Closing

Speaker: Group representative

Script:

That concludes our InsightLearn website walkthrough.

In this video, we demonstrated how users can register, log in, access different modules based on role, browse and enroll in courses, view lessons, track progress, take quizzes, view results, and use dashboard analytics.

The system includes interlinked pages, HTML and CSS layout, JavaScript interaction, SQL Server database connectivity, form validation, admin and student modules, CRUD operations, multimedia lesson content, and dynamic database-driven features.

Thank you for watching.

## Short Backup Script If The Video Is Too Long

If the video is close to 15 minutes, shorten each member section to this pattern:

1. State the module name.
2. Show one main page.
3. Show one validation example.
4. Show one CRUD action.
5. Show one special feature.
6. End with one sentence linking to the marking criteria.

Example:

My module is the Course module. It allows admins to manage courses and students to browse published courses. I will show one validation example, one course insert, one course update, and the publish feature. This demonstrates layout, authorization, dynamic content, CRUD, validation, and usability.

