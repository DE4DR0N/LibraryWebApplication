# Library Web Application

Library Web Application is a modern web application developed using ASP.Net Core and EF Core, designed to manage library resources. The application provides users with a convenient interface for searching, viewing, and managing books and authors.

## Features

- Authentication and Authorization: Utilizes JWT tokens to ensure security and manage user access.
- CRUD Operations: Allows creation, reading, updating, and deletion of book and author records.
- Client-Side Integration: Implements the client-side using React to provide an interactive and responsive user interface.
- Clean Architecture: Easy to update and add new functions.

## Tech Stack

**Client:** React

**Server:** ASP.Net Web Api, EF Core, MS SQL Server

## Installation

Change *LibraryWebAppDbContext* of your database in **appsettings.json**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
    "Issuer": "http://localhost:5059",
    "Audience": "http://localhost:5059",
    "AccessTokenExpiration": 30,
    "RefreshTokenExpiration": 60
  },
  "ConnectionStrings": {
    "LibraryWebAppDbContext": "{your connection string}"
  },
  "AllowedHosts": "*"
}
```

## Deployment

To update the database run

```bash
dotnet ef database update -p .\LibraryWebApp.Persistence -s .\LibraryWebApp.API
```
