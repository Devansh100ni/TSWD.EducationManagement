## Dependency Diagram

                ┌──────────────────────────────┐
                │   TSWD.EducationManagement   │   (API / Presentation)
                └───────────────┬──────────────┘
                                │
                                ▼
                ┌──────────────────────────────┐
                │ TSWD.EducationManagement.App │   (Application Layer)
                └───────┬─────────┬────────────┘
                        │         │
       ┌────────────────┘         └────────────────┐
       ▼                                           ▼
┌──────────────────────────────┐       ┌──────────────────────────────┐
│ TSWD.EducationManagement.EF  │       │ TSWD.EducationManagement.Dapper │  (Infrastructure)
└───────────────┬──────────────┘       └──────────────┬───────────────┘
                │                                     │
                ▼                                     ▼
      ┌──────────────────────────────┐       ┌──────────────────────────────┐
      │   TSWD.EducationManagement   │       │   TSWD.EducationManagement   │
      │           .Domain            │       │           .Domain            │
      └──────────────────────────────┘       └──────────────────────────────┘
                                │
                                ▼
                ┌──────────────────────────────┐
                │   TSWD.EducationManagement   │   (Shared Utilities)
                │            .Shared           │
                └──────────────────────────────┘


## Scaffold Command

Scaffold-DbContext "Server=DESKTOP-6D1OCD8;Database=EducationDb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context EducationDbContext -Project TSWD.EducationManagement.EntityFrameworkCore -StartupProject TSWD.EducationManagement

Scaffold-DbContext "Server=DESKTOP-6D1OCD8;Database=EducationDb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir TempEntities -Context EducationDbContext -Project TSWD.EducationManagement.EntityFrameworkCore -StartupProject TSWD.EducationManagement



1. Create a Permissions Table

This will define individual permissions like:
Add Student
Add Teacher
Add Class
Add Book
etc.

2. Create a RolePermissions Table
This table links Roles to Permissions. You need to make it Tenant-specific, so each tenant can have different role-permission mappings.
Note: This enables multi-tenant role customization, which means "Admin" for Tenant A can have different permissions from "Admin" for Tenant B.

1. Sample Use Case

You want to assign to the Admin role (for Tenant X) the following permissions:
Add Student
Add Teacher
Add Book
Add Class

Workflow:
Insert those permissions into AppPermissions.
Link them to the AppRoles entry for "Admin" + Tenant X in AppRolePermissions.

Benefits of This Approach
Fully dynamic: Add new permissions without schema change.
Multi-tenant aware: Different tenants can customize roles independently.
Fine-grained access control.

AppTenants (Id)
  ↑         ↑
  |         |
AppUsers   AppRoles (Id)
                ↑
         AppRolePermissions
                ↑
         AppPermissions


-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------
                                 SCHOOLS ROADMAP
-----------------------------------------------------------------------------------
-----------------------------------------------------------------------------------



## 🏫 **School Management Roadmap**

### 1. **School Level (Tenant Root)**

Each school is its own entity (in multi-tenant setup, one school = one tenant).

**School has:**

* Basic Info (Name, Code, Address, Contact, Logo)
* Settings (custom per school)
* Academic Sessions
* Departments
* Classes
* Students
* Teachers
* Library
* Attendance System
* Fees & Accounting
* Transport
* Examination System
* Events & Notices

---

### 2. ⚙️ **School Settings**

Every school maintains its own operational settings.

**Example settings:**

* Grading Policy (A+, A, B, etc.)
* Attendance Rules (Full-day, Half-day logic)
* Working Days / Holidays
* Max Students per Class
* Promotion Rules
* Fee Configuration (Monthly, Term-wise, Annual)
* Academic Year Start-End Dates
* Email / Notification Templates
* Branding (School logo, color theme, SMS sender ID)

---

### 3. 🧑‍🏫 **Departments & Staff**

**School → Departments → Teachers → Roles**

**Entities:**

* Department (Science, Commerce, Arts)
* Teacher (Personal Info, Assigned Department)
* Roles (Principal, HOD, Teacher, Librarian)
* Permissions (manage students, grades, etc.)

---

### 4. 🏫 **Classes / Courses / Sections**

Each school defines its own class structure.

**Structure:**

```
School
 └── Classes
       ├── Class Info (e.g., Grade 10)
       ├── Sections (A, B, C)
       ├── Course Mappings
       ├── Timetable
```

**Each class has:**

* Assigned Teachers
* List of Students
* Course subjects (Math, Science, English)
* Timetable
* Capacity limit
* Attendance records

---

### 5. 📚 **Courses / Curriculum**

Each class belongs to specific courses.

**Course details:**

* Course Code / Name
* Syllabus
* Credit Hours
* Assigned Teachers
* Evaluation criteria (Assignments, Exams, Attendance weightage)

---

### 6. 👨‍🎓 **Students**

Each school maintains its own students with relationships.

**Student details:**

* Personal Info
* Enrollment Number
* Class & Section
* Academic History
* Guardian Info
* Attendance Record
* Fee Payment Record
* Library Borrow History
* Exam Results

---

### 7. 🏛 **Library**

Each school has its own library.

**Entities:**

```
School → Library → Books → Borrow Records
```

**Books:**

* Title, Author, ISBN, Category
* Copies Available
* Shelf Location
* Issued Count

**Borrow Records:**

* Borrowed by (Student / Teacher)
* Issue Date / Due Date
* Return Date
* Fine if overdue

---

### 8. 💰 **Fees & Accounting**

Each school defines its fee structure and manages payments.

**Fee Entities:**

* Fee Types (Tuition, Transport, Exam)
* Payment Schedule
* Concessions / Discounts
* Payment Methods (Cash, Card, UPI)
* Transaction History

---

### 9. 🚌 **Transport**

Each school may have its own transport management.

**Entities:**

* Bus Routes
* Pickup / Drop Points
* Assigned Drivers
* Bus Attendance

---

### 10. 🧾 **Examination & Result Management**

Each school defines its own examination flow.

**Structure:**

* Exam Types (Midterm, Final)
* Subjects
* Marks Entry
* Grading Formula
* Report Cards

---

### 11. 🕓 **Attendance System**

Each school tracks attendance separately for teachers and students.

**Entities:**

* Daily Attendance
* Late / Early Marking
* Leave Requests
* Automated Reports

---

### 12. 🗓 **Events, Circulars, and Announcements**

Each school publishes internal notices.

**Entities:**

* Event (Annual Day, PTM)
* Circular (Notices to students/staff)
* Notifications (SMS, Email, Portal)

---

### 13. 📈 **Reports and Analytics**

Every school can generate its own reports.

**Reports include:**

* Student Attendance Summary
* Fee Due Summary
* Exam Performance Analysis
* Library Usage
* Transport Utilization
* Staff Performance
* Daily/Monthly Dashboards

---

### 14. 🔒 **Security & Access**

* User Roles (Admin, Teacher, Student, Parent)
* Role-based Access Control (RBAC)
* Audit Logs (login, updates, etc.)
* Data Isolation (per school / tenant)

---

## 🧩 **Example Relationship Map**

```
School
 ├── Settings
 ├── Departments
 │    └── Teachers
 ├── Classes
 │    ├── Courses
 │    ├── Students
 │    └── Timetable
 ├── Library
 │    ├── Books
 │    └── BorrowRecords
 ├── Fees
 │    └── Payments
 ├── Exams
 │    ├── Subjects
 │    └── Results
 ├── Attendance
 │    ├── Students
 │    └── Staff
 └── Transport
      ├── Routes
      └── Drivers
```
------------------------------------------------------------------------------------------------------------------------------------------------


Here’s a full breakdown 👇

---

## 🏫 **SCHOOL ENTITY (Tenant Root)**

Each school operates independently in your system — so everything below is **scoped per tenant**.

---

## ⚙️ **SCHOOL SETTINGS MODULE**

### 1. **General Settings**

| Setting           | Description                             |
| ----------------- | --------------------------------------- |
| School Name       | Official name of the institution        |
| School Code       | Unique code (auto-generated per tenant) |
| Academic Year     | Current active session                  |
| Logo & Branding   | Logo, color theme, favicon              |
| Language / Locale | Default UI language                     |
| Time Zone         | School timezone                         |
| Date Format       | dd/MM/yyyy or MM/dd/yyyy                |
| Contact Info      | Email, phone, address                   |

---

### 2. **Academic Settings**

| Setting                 | Description                             |
| ----------------------- | --------------------------------------- |
| Grading Policy          | Percentage → Grade (e.g., 90–100 = A+)  |
| Promotion Rules         | Required marks or attendance %          |
| Subjects per Class      | Number of subjects per grade            |  ---
| Evaluation Criteria     | Weightage for exam, assignment, project |  ---
| Class Strength Limit    | Max students per class                  |  ---
| Attendance Rules        | Half-day/full-day logic, grace period   |  ---
| Academic Year Start-End | Auto-calculation for sessions           |  --- 
| Exam Calendar           | Start and end dates for all exams       |  ---

---

### 3. **Fee & Finance Settings**

| Setting                  | Description                    |
| ------------------------ | ------------------------------ |
| Fee Type                 | Monthly / Quarterly / Annual   |
| Fine Rules               | Late fee per day or percentage |
| Payment Methods          | Cash, Card, UPI, Bank Transfer |
| Discounts & Scholarships | Percentage or fixed value      |
| Auto Reminder            | Fee due reminder frequency     |

---

### 4. **Library Settings**

| Setting               | Description                         |
| --------------------- | ----------------------------------- |
| Max Books Per Student | Limit of borrowable books           |
| Borrow Duration       | Days allowed before fine            |
| Fine Per Day          | Amount per late day                 |
| Issue Rules           | Student eligibility (no dues, etc.) |

---

### 5. **Communication & Notification Settings**

| Setting                        | Description                        |
| ------------------------------ | ---------------------------------- |
| SMS Gateway                    | API URL and credentials            |
| Email Configuration            | SMTP server details                |
| Templates                      | Admission, Fee, Attendance, Exam   |
| Parent Notification Preference | SMS, Email, Portal                 |
| Auto Alerts                    | Absence / Fee Due / Event Reminder |

---

### 6. **User Access Settings**

| Setting                   | Description                     |
| ------------------------- | ------------------------------- |
| Roles & Permissions       | Admin, Teacher, Parent, Student |
| Password Policy           | Length, expiry, reset interval  |
| Two-Factor Authentication | Enabled / Disabled              |
| Session Timeout           | Auto logout after inactivity    |

---

## 🧑‍🏫 **DEPARTMENTS MODULE**

Every school has departments to organize academic and administrative units.

### 1. **Academic Departments**

| Department       | Example Subjects / Purpose   |
| ---------------- | ---------------------------- |
| Science          | Physics, Chemistry, Biology  |
| Mathematics      | Algebra, Geometry, Calculus  |
| Commerce         | Accounting, Business Studies |
| Arts             | History, Political Science   |
| Computer Science | Programming, AI, Robotics    |
| Language         | English, French, Hindi       |

---

### 2. **Administrative Departments**

| Department           | Function                      |
| -------------------- | ----------------------------- |
| Administration       | Admission, General Office     |
| Finance & Accounts   | Fee, Budgeting, Payroll       |
| Human Resources      | Teacher Recruitment, Leaves   |
| IT & Infrastructure  | Systems, Networks, Portals    |
| Examination Cell     | Exam scheduling and grading   |
| Transport Department | Bus routes and drivers        |
| Library Department   | Book management               |
| Student Affairs      | Discipline, Counseling        |
| Health & Safety      | First aid, Emergency protocol |

---

## 🏫 **CLASSES MODULE**

| Entity        | Description                        |
| ------------- | ---------------------------------- |
| Class         | Grade or Standard (e.g., Class 10) |
| Section       | Subdivision (A, B, C)              |
| Course        | Subjects assigned to class         |
| Class Teacher | Teacher assigned per class         |
| Timetable     | Weekly period structure            |
| Students      | List of enrolled students          |

---

## 📚 **LIBRARY MODULE**

| Entity        | Description                   |
| ------------- | ----------------------------- |
| Book          | ISBN, Title, Author, Category |
| Book Copies   | Individual book inventory     |
| Borrow Record | Who borrowed, when, due date  |
| Fine Record   | Late returns and penalties    |

---

## 💰 **FINANCE MODULE**

| Entity       | Description                  |
| ------------ | ---------------------------- |
| Fee Type     | Tuition, Transport, Exam Fee |
| Payment      | Records of transactions      |
| Discount     | Applied per student or group |
| Pending Fees | Due tracking                 |
| Refund       | Approved adjustments         |

---

## 🧾 **EXAMINATION MODULE**

| Entity            | Description                |
| ----------------- | -------------------------- |
| Exam Type         | Midterm, Final, Monthly    |
| Subject Mapping   | Per class and course       |
| Marks Entry       | Input by teacher           |
| Result Sheet      | Auto-generated             |
| Grade Calculation | Based on rules in Settings |

---

## 🕓 **ATTENDANCE MODULE**

| Entity             | Description            |
| ------------------ | ---------------------- |
| Daily Attendance   | Students and Staff     |
| Late/Early Records | Check-in/out time      |
| Leave Management   | Requests and approvals |
| Reports            | Monthly and class-wise |

---

## 🚌 **TRANSPORT MODULE**

| Entity          | Description                |
| --------------- | -------------------------- |
| Route           | Pickup and drop paths      |
| Vehicle         | Bus details                |
| Driver          | Assigned staff             |
| Student Bus Map | Who travels on which route |

---

## 🗓 **EVENTS & NOTICES MODULE**

| Entity       | Description                 |
| ------------ | --------------------------- |
| Event        | Annual Day, PTM, Sports Day |
| Circular     | Announcements and updates   |
| Notification | Sent to parents and staff   |
| Calendar     | Consolidated school events  |

---

## 📈 **REPORTS MODULE**

| Entity            | Description              |
| ----------------- | ------------------------ |
| Student Report    | Attendance, Grades, Fees |
| Teacher Report    | Performance, Feedback    |
| Fee Summary       | Collected vs Due         |
| Library Report    | Borrow stats             |
| Transport Report  | Route utilization        |
| Overall Dashboard | School KPIs              |

---

## 🔒 **SECURITY & AUDIT MODULE**

| Entity           | Description                |
| ---------------- | -------------------------- |
| User Roles       | Permissions matrix         |
| Login Audit      | Successful & failed logins |
| Data Log         | Changes in sensitive data  |
| Tenant Isolation | Each school isolated       |

---

Would you like me to generate this as a **visual architecture diagram (like a multi-module system map)** — showing each module (Settings, Departments, Library, etc.) connected under the school tenant?
It’ll look like a **project blueprint** for your `TSWD.EducationManagement` system.
