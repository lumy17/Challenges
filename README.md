# Challenges App

The Challenges App is a .NET Core web application built using C#, Entity Framework, and SQL Server. It allows users to create, manage, and track various challenges.

## Description

The Challenges App provides the following functionality:

**Unauthenticated User**:
- Home page with a motivational quote and a button to navigate to the registration page
- Navigation bar with links to the registration and login pages, as well as a page that lists the available challenges
- To view the details of the challenges, the user must be logged in

**Registration Page**:
- Allows users to register by providing an email, password, and password confirmation
- Users can select the categories of challenges they are interested in

**Login Page**:
- Allows users to log in with their email and password
- Includes links for "Forgot Password?" and "Register as new user"

**Authenticated User**:
- Recommendations for challenges based on the user's selected interest categories
- Dashboard page with information about completed challenges, earned badges, and current tasks
- Challenges page that lists all available challenges with details like image, title, categories, and number of views
- Ability to start a challenge, view the tasks, and mark tasks as completed
- "My Challenges" page that shows the user's active and completed challenges
- "Need Help?" page to contact a chatbot for assistance

**Administrator**:
- Dashboard with statistics on registered users, started challenges, and completed challenges
- Pages to manage challenges, tasks, user challenges, users, badges, and challenge categories

## Technologies Used

- **Framework**: ASP.NET Core running on runtime .NET Core
- **Language**: C#
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Frontend**: Razor Pages with HTML, CSS, JavaScript
- **Authentication**: Identity Framework
- **Development Environment**: Visual Studio
- **Deployment**: Azure App Service

## Getting Started

1. **Clone the repository**:
   ```bash git clone (https://github.com/lumy17/Challenges.git)```
2. **Configure the database**:
- Open the solution in Visual Studio
- Update the connection string in the `appsettings.json` file to point to your SQL Server instance
- Run the database migrations using the Package Manager Console:
  ```
  Add-Migration name
  Update-Database
  
  ```

3. **Build and run the application**:
- Press `F5` in Visual Studio to build and run the application
