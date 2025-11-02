POWA Digital Transformation System
A secure, cloud-integrated platform developed for People Opposing Women Abuse (POWA) to digitize survivor support, advocacy tracking, and governance across multiple branches.

Project Overview
This system replaces manual workflows with a centralized digital solution, enabling:
-Secure intake and case tracking for survivors
-Role-based dashboards for staff, volunteers, and board members
-Real-time data sync between local SQL Server and Azure-hosted services
-Automated reporting and milestone tracking

 How to Get the Project from GitHub
-Make sure Git is installed on your computer
-Open your terminal or command prompt
-Type this command and press Enter
-git clone [https://github.com/Imp2016-2020/PowaManagementSystem.git]
-Go into the project folder
-cd PowaManagementSystem
-Open the project in Visual Studio
-Update the database connection in appsettings.json
-Run the project (press F5 or click "Start")
-Done! You can now work on the system locally



Client Profile
-Organization: People Opposing Women Abuse (POWA)
-Mission: Support survivors of gender-based violence and advocate for systemic change
-Services: Counseling, sheltering, legal aid, skills development, policy reform

System Objectives
-Digitize survivor intake and referral tracking
-Implement secure role-based access control (RBAC)
-Track advocacy efforts and donor/partner engagement
-Enable smart decision-making via real-time dashboards
-Ensure audit-ready logs and POPIA compliance

Technology Stack
-.NET MVC – Used as the backend architecture to structure the application using the Model-View-Controller pattern.
-Microsoft Azure – Provides cloud hosting, App Services, and Azure SQL Database for secure and scalable deployment.
-SQL Server & MySQL Workbench – Utilized for relational database design, ER modeling, and performance tuning.
-GitHub & Azure DevOps – Supports version control, CI/CD pipelines, and collaborative development workflows.
-Visual Studio – Primary integrated development environment (IDE) for coding, debugging, and managing the project.
-Microsoft Word – Used for creating and maintaining system documentation and stakeholder reports.

Deployment
Hosted on Microsoft Azure App Services with integrated Azure SQL Database. Supports remote access, role-based security, and CI/CD pipelines via Azure DevOps.

Architecture & Patterns
-MVC Pattern for modular, testable design
-RBAC Pattern for secure role-based access
-Repository Pattern for clean data access
-Association Class Pattern for complex entity relationships
-Observer Pattern (planned) for real-time notification

 User Roles
-Admin
-Social Worker
-Volunteer
-Board Member
Each role has tailored access and UI rendering to ensure confidentiality and usability

Agile Planning
- Methodology: Agile Scrum
- Epics: Foundation & Security, Project Management, Community Management, Reporting
- User Stories: Defined per sprint with states (New, To Do, Committed, In Progress, Completed)

