# InsightLearn 📚

A full-featured e-learning platform built with ASP.NET Web Forms as a university Web Technology project.

---

## 👥 Team Members

| Name | Role |
|------|------|
| Ng Ern Chi | Project Lead |
| Oswald Loh Kar Tzun | Backend Developer |
| Chan Kar Jun | Frontend Developer |
| Ian Lim | UI/UX Designer |
| Foo Kim Chean | Database Engineer |

---

## ✨ Features

- **Student**
  - Register and log in
  - Browse and enrol in courses
  - Watch video lessons and read content
  - Take multiple-choice quizzes with instant scoring
  - Track lesson completion and course progress
  - Earn and download a certificate on course completion
  - Manage account settings (display name, password)

- **Admin**
  - Manage users (add, edit, delete)
  - Manage courses (add, edit, delete)
  - Manage lessons with video URL support
  - Manage quizzes and questions
  - View dashboard with site statistics

---

## 🛠️ Technology Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Web Forms (.NET Framework 4.7.2) |
| Database | SQL Server LocalDB + ADO.NET |
| Frontend | HTML5, CSS3 (Flexbox & Grid), JavaScript |
| IDE | Visual Studio 2022 |

---

## 📁 Project Structure

```
InsightLearn/
│
├── App_Data/               # SQL Server LocalDB database files (.mdf, .ldf)
├── Images/                 # Team member photos
├── Scripts/                # JavaScript (scripts.js)
├── Styles/                 # CSS files (split by team member)
│   ├── base.css            # Variables, reset, utility — Ng Ern Chi
│   ├── layout.css          # Navbar, footer, responsive — Chan Kar Jun
│   ├── components.css      # Buttons, cards, forms, shared — Ian Lim
│   ├── student-pages.css   # Student-facing pages — Foo Kim Chean
│   └── admin-pages.css     # Admin-facing pages — Oswald Loh Kar Tzun
│
├── Site.master             # Master page for student-facing pages
├── AdminSite.master        # Master page for admin pages
│
├── Default.aspx            # Home page
├── Login.aspx              # Login
├── Register.aspx           # Registration
├── CourseList.aspx         # Browse courses
├── Lesson.aspx             # View lesson (video + content)
├── Quiz.aspx               # Take quiz
├── QuizResult.aspx         # Quiz results
├── Certificate.aspx        # Course certificate
├── StudentDashboard.aspx   # Student dashboard
├── Settings.aspx           # Account settings
├── About.aspx              # About page
│
├── AdminDashboard.aspx     # Admin dashboard
├── AdminManageUsers.aspx   # Manage users
├── AdminManageCourses.aspx # Manage courses
├── AdminManageLessons.aspx # Manage lessons
├── AdminManageQuizzes.aspx # Manage quizzes
├── AdminManageQuestions.aspx # Manage quiz questions
│
├── Web.config              # App configuration + DB connection string
└── database.sql            # Database schema + seed data
```

---

## ⚙️ Setup Instructions

### Prerequisites
- Visual Studio 2022 (with **ASP.NET and web development** workload)
- SQL Server Express / LocalDB (included with Visual Studio)

### Steps

**1. Clone the repository**
```bash
git clone https://github.com/Priscilla0117/InsightLearn.git
```

**2. Open the project**
- Open Visual Studio
- File → Open → Project/Solution
- Select `InsightLearn.sln` (or open the folder and Visual Studio will detect it)

**3. Set up the database**

Option A — Use the included `.mdf` file (easiest):
- The `App_Data/InsightLearn.mdf` file is already included
- Visual Studio attaches it automatically when you run the project

Option B — Create from the SQL script:
- Open SQL Server Management Studio (SSMS) or the VS SQL Server Object Explorer
- Create a new database named `InsightLearn`
- Run `database.sql` to create tables and insert seed data
- Update the connection string in `Web.config` if needed

**4. Run the project**
- Press **F5** in Visual Studio
- The site opens at `https://localhost:XXXX/`

### Default Login Accounts

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@insightlearn.com | admin123 |
| Student | student@example.com | student123 |

> ℹ️ Check `database.sql` for the full list of seed accounts.

---

## 🗄️ Database Schema

Main tables:

| Table | Description |
|-------|-------------|
| `Users` | Student and admin accounts |
| `Courses` | Course catalogue |
| `Lessons` | Lessons belonging to courses |
| `Enrollment` | Which students enrolled in which courses |
| `Lesson_Progress` | Per-student lesson completion tracking |
| `Quizzes` | Quizzes linked to courses |
| `Questions` | Quiz questions (multiple choice) |
| `Quiz_Attempts` | Student quiz attempt history |

---

## 📝 Notes

- The project uses **ASP.NET Web Forms** — no MVC, no Razor Pages
- Passwords are stored as **SHA-256 hashes** in the database
- CSS is split into 5 files (one per team member) loaded via the master pages
- Videos are embedded via YouTube iframe (use YouTube embed URLs in admin panel)
