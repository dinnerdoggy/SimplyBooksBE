# 📚 SimplyBooks API

## Welcome to the SimplyBooks API — a RESTful web service for managing authors and their books, built with ASP.NET Core and Entity Framework Core. This API supports full CRUD operations and is designed to serve as the backend for book-focused applications.




## Getting Started

### 1. Prerequisites
- .NET 8 SDK

- PostgreSQL

- Postman (for testing)

- PgAdmin (optional, for GUI access to PostgreSQL)

### 2. Set Your Connection String
- Create a file named appsettings.Development.json in the root of the project (next to Program.cs), and add your PostgreSQL connection string:

{ "SimplyBooksBEDbConnectionString": "Host=localhost;Port=5432;Database=SimplyBooksBE;Username=postgres;Password=yourpassword" }

- ⚠️ This file is included in .gitignore to prevent secrets from being committed.

### 3. Apply Migrations & Seed the Database
- This project uses Entity Framework Core to manage the database schema and seed initial data. Run the following command:

- dotnet ef database update

This will:

- Create the database (if it doesn't exist)

- Apply the current schema

- Insert initial authors and books

### 4. Run the Application
Start the server with:

- dotnet run

Once running, the API will be available at:

- https://localhost:7257

Swagger (interactive API docs) will be available at:

- https://localhost:7257/swagger

## API Testing
- Postman collection with documentation available [here.](https://documenter.getpostman.com/view/36624789/2sB2cUCPM9)

Authentication
This API is designed to integrate with Firebase Authentication. While authentication is not enforced in this version, the uid field on authors and books can be used to associate records with specific users.

In a production version, Firebase token validation middleware would secure routes based on the authenticated user.

Testing the API
You can test all endpoints using:

Swagger UI
https://localhost:7257/swagger

Postman
Send JSON requests manually to the base URL using your preferred method.

Technologies Used
ASP.NET Core 8 (Minimal APIs)

Entity Framework Core 8

PostgreSQL

Swagger / Swashbuckle

LINQ & async/await

Clean RESTful architecture

#### Features
- Full CRUD for authors and books

- One-to-many relationship: each book belongs to an author

- Input trimming and validation for clean, reliable data

- Descriptive error responses and developer-friendly feedback

- Firebase authentication integration

Contact
Built by [Casey Cunningham].
For questions, suggestions, or contributions, feel free to open an issue or contact me at dinnerdoggy@gmail.com.
