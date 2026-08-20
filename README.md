<div align="center">

# Flappy Clouds

### A full-stack e-commerce platform for crochet, handmade jewelry, and custom gifts

Built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=flat-square&logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-EF_Core-CC2927?style=flat-square&logo=microsoftsqlserver)
![Tests](https://img.shields.io/badge/Tests-MSTest-25A162?style=flat-square)
![License](https://img.shields.io/badge/License-GPL--3.0-blue?style=flat-square)

</div>

## Overview

Flappy Clouds is a full-stack storefront designed for browsing and purchasing handmade products such as crochet pieces, bracelets, phone charms, keychains, and plushies. It combines a customer-facing catalog and checkout flow with account management and an administrative dashboard for managing products, users, and orders.

## Highlights

- Product catalog with categories, pagination, stock information, and related-item suggestions
- Product search with autocomplete suggestions
- Registration and cookie-based authentication with securely hashed passwords
- Shopping cart with quantity updates and item removal
- Guest checkout and order creation
- Role-protected admin dashboard
- Product, user, and order management
- Product image uploads
- Entity Framework Core migrations for database setup
- MSTest coverage for registration, login, invalid credentials, and logout behavior

## Product Gallery

<div align="center">
  <img src="./Flappy%20Clouds/wwwroot/uploads/products/Plushie-all.jpg" alt="Crochet plushies" width="31%" />
  <img src="./Flappy%20Clouds/wwwroot/uploads/products/Bracelet-all.png" alt="Handmade bracelets" width="31%" />
  <img src="./Flappy%20Clouds/wwwroot/uploads/products/Phone-Charm-3.png" alt="Handmade phone charm" width="31%" />
</div>

## Tech Stack

| Layer | Technologies |
| --- | --- |
| Backend | C#, .NET 9, ASP.NET Core MVC |
| Data | Entity Framework Core 9, SQL Server |
| Frontend | Razor Views, HTML, CSS, JavaScript, Bootstrap, jQuery |
| Authentication | ASP.NET Core cookie authentication, PasswordHasher |
| Testing | MSTest, Moq, EF Core InMemory |
| Tooling | Visual Studio, NuGet, EF Core migrations |

## Project Structure

```text
Flappy-Clouds/
├── Flappy Clouds/
│   ├── Controllers/       # Customer, account, cart, product, and admin flows
│   ├── Entities/          # Database entities and EF Core context
│   ├── Migrations/        # Database schema history
│   ├── Models/            # View models and form models
│   ├── Views/             # Razor UI
│   ├── wwwroot/           # CSS, JavaScript, icons, and product imagery
│   └── Program.cs         # Application configuration and middleware
├── FlappyCloudsTest/      # MSTest project
└── Flappy Clouds.sln      # Visual Studio solution
```

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server
- EF Core command-line tools, if you plan to apply migrations from the terminal

### 1. Clone the repository

```bash
git clone https://github.com/steezy-archv/Flappy-Clouds.git
cd Flappy-Clouds
```

### 2. Configure the database

The default configuration expects a local SQL Server instance and uses Windows authentication:

```text
Data Source=localhost;Initial Catalog=FlappyClouds;Integrated Security=True;Encrypt=True;Trust Server Certificate=True
```

If your SQL Server setup is different, update `ConnectionStrings:Default` in `Flappy Clouds/appsettings.json`.

### 3. Apply the migrations

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project "Flappy Clouds/Flappy Clouds.csproj"
```

### 4. Run the application

```bash
dotnet run --project "Flappy Clouds/Flappy Clouds.csproj"
```

Open the local URL shown in the terminal.

## Running Tests

```bash
dotnet test "Flappy Clouds.sln"
```

The test project uses an in-memory database and mocked password hashing to exercise the account controller.

## License

This project is distributed under the [GNU General Public License v3.0](LICENSE).
