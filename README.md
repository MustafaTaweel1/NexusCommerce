# NexusCommerce (ASP.NET Core E-Commerce Store) 🛒⚡
> A robust, modern E-Commerce web application built with **ASP.NET Core 8.0 (MVC)**, **C#**, **Entity Framework Core 8.0**, **SQL Server**, and **ASP.NET Core Identity** utilizing an **N-Tier Architecture with Repository & Unit of Work patterns**.

---

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4.svg?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120.svg?style=flat&logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET%20Core-MVC%20%2F%20Razor-512BD4.svg?style=flat)](https://learn.microsoft.com/aspnet/core)
[![EF Core](https://img.shields.io/badge/ORM-EF%20Core%208.0-512BD4.svg?style=flat)](https://learn.microsoft.com/ef/core)
[![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC292B.svg?style=flat&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Bootstrap 5](https://img.shields.io/badge/UI-Bootstrap%205-7952B3.svg?style=flat&logo=bootstrap)](https://getbootstrap.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg?style=flat)]()

---

## 📖 Overview

**NexusCommerce** (formerly *Store*) is a full-featured online store and back-office management system engineered using enterprise **N-Tier Clean Architecture**. 

The platform separates responsibilities into distinct domain, data access, utility, and web presentation layers. It empowers customers to browse multi-category catalogs, view comprehensive product specs, and manage shopping carts, while providing administrators with a dashboard for inventory, product image uploads, and company accounts.

---

## 🌟 Key Features

### 🛍️ 1. Customer Storefront
- **Product Catalog**: Responsive grid displaying product cards, pricing, brand tags, and category classifications.
- **Product Details**: Dedicated product preview page with high-resolution imagery and specifications.
- **Shopping Cart**: Item quantity selection and cart persistence linked to customer accounts.

### 🛡️ 2. Admin Back-Office Management
- **Product Management (Upsert)**:
  - Create and edit products using a unified Upsert workflow.
  - Automatic image handling (upload, file validation, storage, and replacement).
  - Rich-text product descriptions powered by **TinyMCE**.
- **Interactive DataTables**: Fast, searchable, paginated AJAX product tables with dynamic sorting.
- **SweetAlert2 Deletions**: Asynchronous delete operations with animated confirmation dialogs.
- **Category Management**: Create, edit, reorder, and remove product categories with custom display ordering.
- **Company Management**: Manage corporate B2B client profiles, addresses, and contacts.

### 🔐 3. Authentication & Role-Based Authorization
- **ASP.NET Core Identity**:
  - Secure registration, login, email confirmation tokens, and password recovery.
  - Role-based authorization (`Admin`, `Customer`, `Company`, `Employee`).
  - Automatic navigation and menu filtering based on assigned user permissions.

---

## 🏗️ N-Tier Architecture & Design Patterns

The solution is divided into 4 decoupled projects:

```
                          ┌──────────────────────────────────────┐
                          │              Store_Web               │
                          │   (Presentation Layer - MVC & Razor) │
                          └──────────────────┬───────────────────┘
                                             │
                       ┌─────────────────────┼─────────────────────┐
                       │                     │                     │
                       ▼                     ▼                     ▼
        ┌──────────────────────────┐ ┌───────────────┐ ┌──────────────────────┐
        │     Store.DataAccess     │ │ Store.Utility │ │     Store.Models     │
        │(EF Core, Repositories,   │ │ (Constants,   │ │ (Domain Entities &   │
        │ Unit of Work, Migrations)│ │ Email Sender) │ │  ViewModels)         │
        └──────────────┬───────────┘ └───────────────┘ └──────────────────────┘
                       │
                       ▼
        ┌──────────────────────────┐
        │   Microsoft SQL Server   │
        └──────────────────────────┘
```

### Applied Patterns
- **Repository Pattern (`IRepository<T>`)**: Encapsulates data access and decoupling from ORM.
- **Unit of Work Pattern (`IUnitOfWork`)**: Manages transactions across multiple repositories atomically.
- **Area-Based Separation**: Decouples `Admin`, `Customer`, and `Identity` modules.
- **View Models (`ProductVM`)**: Strongly typed data transfer between controllers and views.

---

## 📁 Project Structure

```text
Store/
├── Store.Models/                       # Domain Entities & ViewModels
│   ├── AppUser.cs                      # Extended IdentityUser with addresses & company link
│   ├── Category.cs                     # Product category entity & display order
│   ├── Company.cs                      # B2B Corporate client entity
│   ├── Product.cs                      # Product details, pricing, foreign keys & image URL
│   ├── ShoppingCart.cs                 # User shopping cart items & counts
│   └── ViewModels/
│       ├── ProductVM.cs                # Product creation/edit ViewModel with dropdown lists
│       └── ErrorViewModel.cs           # Error diagnostic model
├── Store.DataAccess/                   # Data Access & Persistence Layer
│   ├── Data/
│   │   └── AppDbContext.cs             # EF Core IdentityDbContext & DbSet registrations
│   ├── Migrations/                     # Entity Framework Core database migrations
│   └── Repository/                     # Generic & entity-specific repositories
│       ├── IRepository/                # Generic IRepository<T> & specific interfaces
│       ├── Repository.cs               # Base CRUD repository implementation
│       ├── ProductRepository.cs        # Product custom updates
│       ├── CategoryRepository.cs       # Category updates
│       ├── CompanyRepository.cs        # Company updates
│       ├── AppUserRepository.cs        # User data queries
│       ├── ShoppingCartRepository.cs   # Shopping cart queries
│       └── UnitOfWork.cs               # Centralized transaction coordinator
├── Store.Utility/                      # Cross-Cutting Utilities & Helpers
│   ├── SD.cs                           # Static definitions (User Roles: Admin, Customer, etc.)
│   └── EmailSender.cs                  # Identity email notification service
├── Store_Web/                          # ASP.NET Core Presentation Layer
│   ├── Areas/
│   │   ├── Admin/                      # Back-office Admin controllers & Razor views
│   │   │   └── Controllers/            # ProductController, CategoryController, CompanyController
│   │   ├── Customer/                   # Storefront controllers & Razor views
│   │   │   └── Controllers/            # HomeController (Catalog & Details)
│   │   └── Identity/                   # ASP.NET Core Identity authentication pages
│   ├── Views/Shared/                   # _Layout, _Notification, _LoginPartial
│   ├── wwwroot/                        # Static assets (images, custom CSS, JS, DataTables)
│   ├── appsettings.json                # Database connection string & configurations
│   └── Program.cs                      # Dependency Injection & middleware pipeline
└── Store.sln                           # Visual Studio Solution File
```

---

## 🗄️ Database Entities

| Entity | Key Properties | Purpose |
| :--- | :--- | :--- |
| **`Product`** | `Id`, `Name`, `Description`, `Brand`, `price`, `ImageUrl`, `CategortId` | Catalog inventory items |
| **`Category`** | `Id`, `Name`, `DisplayOrder` | Product categorization and filtering |
| **`Company`** | `Id`, `Name`, `StreetAddress`, `City`, `State`, `PostalCode`, `PhoneNumber` | Corporate accounts |
| **`AppUser`** | Inherits `IdentityUser`, `name`, `Address`, `CompanyId` | Customers and administrative accounts |
| **`ShoppingCart`** | `Id`, `ProductId`, `AppUserId`, `Count` | Active user cart items |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (with ASP.NET & web development workload) or [VS Code](https://code.visualstudio.com/) with C# Dev Kit
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or SQL Server Developer)

---

### Installation & Run

1. **Clone the repository**:
   ```bash
   git clone https://github.com/MustafaTaweel1/Store.git
   cd Store
   ```

2. **Configure Connection String**:
   Open [`Store_Web/appsettings.json`](file:///c:/Users/Mustafa/Desktop/testopencode/Store/Store_Web/appsettings.json) and verify your SQL Server connection string:
   ```json
   {
     "ConnectionStrings": {
       "SqlCon": "Server=(localdb)\\mssqllocaldb;Database=StoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Apply Database Migrations**:
   Run the following command using .NET CLI:
   ```bash
   dotnet ef database update --project Store.DataAccess --startup-project Store_Web
   ```
   *(Or in Visual Studio Package Manager Console: `Update-Database -Project Store.DataAccess`)*

4. **Build and Run**:
   ```bash
   dotnet run --project Store_Web
   ```
   Open `https://localhost:7000` or `http://localhost:5000` in your browser.

---

## 📦 Third-Party Libraries & Plugins

- **Frontend**: [Bootstrap 5](https://getbootstrap.com/), [jQuery](https://jquery.com/)
- **Data Tables**: [DataTables.net](https://datatables.net/) (with responsive AJAX data loading)
- **Rich Text Editor**: [TinyMCE](https://www.tiny.cloud/) (for rich product descriptions)
- **Alerts & Modals**: [SweetAlert2](https://sweetalert2.github.io/) (for smooth delete confirmations)

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
