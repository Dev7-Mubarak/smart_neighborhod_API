# Smart Neighborhood API

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-512BD4?style=for-the-badge&logo=dotnet)
![EF Core](https://img.shields.io/badge/EF_Core-8.0-blue?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![xUnit](https://img.shields.io/badge/Tests-xUnit-green?style=for-the-badge)
![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Architecture & Design Patterns](#architecture--design-patterns)
3. [Tech Stack](#tech-stack)
4. [Folder Structure](#folder-structure)
5. [Core Modules & Features](#core-modules--features)
6. [Getting Started](#getting-started)
7. [Configuration Reference](#configuration-reference)
8. [Running the Application](#running-the-application)
9. [API Documentation](#api-documentation)
10. [Testing](#testing)

---

## Project Overview

**Smart Neighborhood API** is a production-grade RESTful backend built with **ASP.NET Core 8** for managing residential communities at scale. It serves as the central system for neighborhood administrators, residents, and government institutions to interact with a shared digital platform.

The API covers the full lifecycle of a managed residential environment:

- **Residential Management** — Neighborhoods, blocks, residential units, and their hierarchical relationships.
- **Family & Member Registry** — Detailed records of families, individual members, family categories, and member roles.
- **Issue Tracking** — Community-raised issues with full status-transition workflows (Open → In Progress → Resolved → Closed).
- **Conflict Resolution** — Conflict cases and conflict type classification with dedicated resolution workflows.
- **Project & Team Management** — Community projects, project categories, team formation, and team member assignment.
- **Notifications** — Multi-channel notification delivery (In-App, Email, Push).
- **Reporting** — On-demand report generation in multiple formats (PDF, Excel, CSV).
- **Government Institution Directory** — A registry of local government institutions and their operational contacts.
- **Dashboard & Analytics** — Aggregated metrics and statistics for administrators.

---

## Architecture & Design Patterns

The system is built on a **clean, layered REST API architecture** with a strong focus on SOLID principles and testability.

### Layered Architecture

```
Controllers  →  Services  →  Repositories / DbContext
                  ↑
             Interfaces (Abstractions)
```

Every service is accessed through its interface, enabling full decoupling and straightforward unit testing with mocks.

### Factory Method Pattern

A cornerstone of this release is the adoption of the **Factory Method Pattern** across three critical domains. This design decouples the creation of concrete strategy objects from the business logic that consumes them, making the system highly extensible without modification.

---

#### 1. `IssueStatusHandlerFactory`

Resolves the correct issue-state handler at runtime based on the current `IssueStatus` enum value.

| Status | Handler |
|---|---|
| `Open` | `OpenIssueHandler` |
| `InProgress` | `InProgressIssueHandler` |
| `Resolved` | `ResolvedIssueHandler` |
| `Closed` | `ClosedIssueHandler` |

**Interface:** `IIssueStatusHandlerFactory` → `IIssueStatusHandler`

```csharp
// Usage in IssueService
var handler = _factory.Create(IssueStatus.Resolved);
await handler.HandleAsync(issue);
```

---

#### 2. `NotificationFactory`

Resolves the correct notification sender at runtime based on the target `NotificationChannel`.

| Channel | Sender |
|---|---|
| `Push` | `PushNotificationSender` |
| `Email` | `EmailNotificationSender` |
| `InApp` | `InAppNotificationSender` |

**Interface:** `INotificationFactory` → `INotificationSender`

```csharp
// Usage in notification workflows
var sender = _factory.Create(NotificationChannel.Email);
await sender.SendAsync(recipient, payload);
```

---

#### 3. `ReportGeneratorFactory`

Resolves the correct report generator at runtime based on the requested `ReportFormat`.

| Format | Generator |
|---|---|
| `Pdf` | `PdfReportGenerator` |
| `Excel` | `ExcelReportGenerator` |
| `Csv` | `CsvReportGenerator` |

**Interface:** `IReportGeneratorFactory` → `IReportGenerator`

```csharp
// Usage in DashboardService
var generator = _factory.Create(ReportFormat.Excel);
var fileBytes = await generator.GenerateAsync(data);
```

---

All three factories are powered by **ASP.NET Core's native DI container**, meaning concrete implementations are resolved via `IServiceProvider`, preserving their own dependency graphs completely.

---

### Additional Patterns

- **Repository / Service Layer** — All business logic is encapsulated in scoped services, isolated from the HTTP layer.
- **API Versioning** — All endpoints are versioned under the `/api/v1/` route prefix via `Microsoft.AspNetCore.Mvc.Versioning`.
- **Global Exception Handling** — Centralized middleware intercepts unhandled exceptions and returns consistent `ProblemDetails`-compliant error responses.
- **Rate Limiting** — Built-in ASP.NET Core rate limiting is applied to protect public endpoints.
- **Pagination** — All list endpoints return `PaginatedResult<T>` to prevent unbounded data fetches.

---

## Tech Stack

| Category | Technology |
|---|---|
| **Framework** | ASP.NET Core 8 |
| **ORM** | Entity Framework Core 8 |
| **Database** | Microsoft SQL Server |
| **Authentication** | ASP.NET Core Identity + JWT Bearer |
| **Logging** | Serilog (Console + Rolling File Sink) |
| **Error Monitoring** | Sentry.AspNetCore |
| **Mapping** | AutoMapper |
| **API Documentation** | Swashbuckle (Swagger UI) + Annotations |
| **API Versioning** | Microsoft.AspNetCore.Mvc.Versioning |
| **AI Integration** | OpenAI SDK |
| **Unit Testing** | xUnit + Moq |
| **Test DB** | EF Core InMemory Provider |
| **Code Coverage** | Coverlet |

---

## Folder Structure

```
smart_neighborhod_API/
│
├── SmartNeighborhoodAPI/                  # Main API project
│   ├── Controllers/
│   │   └── V1/                            # Versioned API controllers
│   │       ├── AuthController.cs
│   │       ├── IssuesController.cs
│   │       ├── ConflictCaseController.cs
│   │       ├── ResidentialNeighborhoodsController.cs
│   │       ├── BlocksController.cs
│   │       ├── FamilyController.cs
│   │       ├── ProjectController.cs
│   │       ├── DashboardController.cs
│   │       └── ...
│   │
│   ├── Services/                          # Business logic layer
│   │   ├── IssueStatusHandlers/           # Factory Method: Issue state handlers
│   │   │   ├── IssueStatusHandlerFactory.cs
│   │   │   ├── OpenIssueHandler.cs
│   │   │   ├── InProgressIssueHandler.cs
│   │   │   ├── ResolvedIssueHandler.cs
│   │   │   └── ClosedIssueHandler.cs
│   │   │
│   │   ├── Factories/                     # Factory Method: Notification & Report factories
│   │   │   ├── NotificationFactory.cs
│   │   │   └── ReportGeneratorFactory.cs
│   │   │
│   │   ├── Notifications/                 # Concrete notification senders
│   │   │   ├── EmailNotificationSender.cs
│   │   │   ├── PushNotificationSender.cs
│   │   │   └── InAppNotificationSender.cs
│   │   │
│   │   ├── ReportGenerators/              # Concrete report generators
│   │   │   ├── PdfReportGenerator.cs
│   │   │   ├── ExcelReportGenerator.cs
│   │   │   └── CsvReportGenerator.cs
│   │   │
│   │   ├── IssueService.cs
│   │   ├── ConflictCaseService.cs
│   │   ├── AuthService.cs
│   │   ├── DashboardService.cs
│   │   └── ...
│   │
│   ├── Interfaces/                        # Service & factory abstractions
│   │   ├── IIssueStatusHandlerFactory.cs
│   │   ├── IIssueStatusHandler.cs
│   │   ├── INotificationFactory.cs
│   │   ├── INotificationSender.cs
│   │   ├── IReportGeneratorFactory.cs
│   │   ├── IReportGenerator.cs
│   │   └── ...
│   │
│   ├── Entites/                           # Domain entity models
│   │   ├── Enums/
│   │   ├── Issue.cs
│   │   ├── Family.cs
│   │   ├── ConfilctCase.cs
│   │   └── ...
│   │
│   ├── Configuration/                     # EF Core Fluent API configurations & seed data
│   ├── Helpers/                           # DTOs, AutoMapper profiles, utilities, JWT config
│   ├── Middlewares/                       # Global exception handler, performance logging
│   ├── Migrations/                        # EF Core migration history
│   ├── Program.cs                         # Application entry point & DI composition root
│   ├── DependencyInjection.cs             # Extension methods for service registration
│   └── appsettings.json
│
├── SmartNeighborhoodAPI.Tests/            # xUnit test project
│   └── Services/
│       ├── Factories/
│       │   ├── NotificationFactoryTests.cs
│       │   └── ReportGeneratorFactoryTests.cs
│       ├── IssueStatusHandlers/
│       │   ├── IssueStatusHandlerFactoryTests.cs
│       │   └── ResolvedIssueHandlerTests.cs
│       ├── ReportGenerators/
│       │   └── PdfReportGeneratorTests.cs
│       └── DashboardServiceExportTests.cs
│
└── SmartNeighborhoodAPI.sln
```

---

## Core Modules & Features

### Residential Management

Manage the full hierarchy of a residential community: **Neighborhoods → Blocks → Residential Units**. Each level carries its own metadata, population counts, and geographical identifiers.

### Family & Member Registry

A comprehensive registry of **Families**, **Family Members**, **Family Categories**, and **Member Roles** (e.g., head of family). Supports adding, updating, and transferring members across units.

### Issue Tracking

Residents and administrators can raise community **Issues** (e.g., infrastructure problems, service complaints). Each issue follows a strict status lifecycle managed by the `IssueStatusHandlerFactory`:
`Open → In Progress → Resolved → Closed`

### Conflict Resolution

Records and manages **Conflict Cases** between parties in the neighborhood, categorized by **Conflict Type**. Supports full CRUD operations and status management.

### Project & Team Management

Supports community **Projects** organized by category, with dedicated **Teams** and **Team Members** assigned to each project. Tracks roles within teams.

### Notification System

Multi-channel notification delivery via the `NotificationFactory`, supporting **In-App**, **Email**, and **Push** channels — each implemented as a separate, independently testable sender.

### Report Generation

On-demand report export through the `ReportGeneratorFactory`, producing output in **PDF**, **Excel (.xlsx)**, and **CSV** formats from dashboard and analytical data.

### Government Institution Directory

A searchable directory of **Government Institutions** and their **Contacts**, enabling residents and administrators to reach relevant authorities.

### Authentication & Authorization

Full **JWT-based authentication** with **ASP.NET Core Identity**, supporting role-based access control (RBAC) across all endpoints.

### Dashboard & Analytics

Aggregated statistics and metrics accessible to administrators, with export capabilities powered by the `ReportGeneratorFactory`.

---

## Getting Started

### Prerequisites

Ensure the following are installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (2019 or later) or a compatible Azure SQL instance
- [Git](https://git-scm.com/)

---

### 1. Clone the Repository

```bash
git clone https://github.com/Dev7-Mubarak/smart_neighborhod_API.git
cd smart_neighborhod_API
```

---

### 2. Configure `appsettings.json`

Open `SmartNeighborhoodAPI/appsettings.json` and update the following sections with your environment's values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER; Database=SmartNeighborhoodDB; Trusted_Connection=True; TrustServerCertificate=True;"
  },
  "Jwt": {
    "Issuer": "SmartNeighborhoodAPI",
    "Audience": "SmartNeighborhoodWebClient",
    "SigningKey": "YOUR_STRONG_SECRET_KEY_AT_LEAST_32_CHARS",
    "DurationInDays": 7
  },
  "EmailSettings": {
    "Host": "smtp.your-provider.com",
    "Port": 587,
    "EnableSSL": true,
    "Username": "your-email@example.com",
    "Password": "your-app-password"
  }
}
```

> **Security Note:** Never commit real credentials to source control. Use **User Secrets** or **environment variables** for production deployments.

To use .NET User Secrets locally:

```bash
cd SmartNeighborhoodAPI
dotnet user-secrets init
dotnet user-secrets set "Jwt:SigningKey" "YOUR_STRONG_SECRET_KEY"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
```

---

### 3. Apply EF Core Migrations

Navigate to the solution root and run:

```bash
dotnet ef database update --project SmartNeighborhoodAPI/SmartNeighborhoodAPI.csproj
```

This will create the database schema and apply all seed data (roles, default users, lookup data).

> If the `dotnet-ef` tool is not installed globally, install it first:
>
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

### 4. Restore Dependencies

```bash
dotnet restore
```

---

## Running the Application

```bash
cd SmartNeighborhoodAPI
dotnet run
```

By default, the API will be available at:

- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`

For the **Development** environment, Swagger UI is served at:

```
https://localhost:5001/swagger
```

---

## API Documentation

The API is fully documented via **Swagger UI**. All endpoints are versioned under `/api/v1/`.

| Domain | Base Route |
|---|---|
| Authentication | `/api/v1/auth` |
| Residential Neighborhoods | `/api/v1/residential-neighborhoods` |
| Blocks | `/api/v1/blocks` |
| Residential Units | `/api/v1/residential-units` |
| Families | `/api/v1/families` |
| Family Members | `/api/v1/family-members` |
| Issues | `/api/v1/issues` |
| Conflict Cases | `/api/v1/conflict-cases` |
| Projects | `/api/v1/projects` |
| Teams | `/api/v1/teams` |
| Government Institutions | `/api/v1/government-institutions` |
| Dashboard | `/api/v1/dashboard` |

---

## Testing

The `SmartNeighborhoodAPI.Tests` project contains unit tests written with **xUnit** and **Moq**, covering the Factory Method implementations, issue status handlers, and report generators.

### Run All Tests

```bash
dotnet test SmartNeighborhoodAPI.Tests/SmartNeighborhoodAPI.Tests.csproj
```

### Run with Detailed Output

```bash
dotnet test SmartNeighborhoodAPI.Tests/SmartNeighborhoodAPI.Tests.csproj --logger "console;verbosity=detailed"
```

### Run with Code Coverage (Coverlet)

```bash
dotnet test SmartNeighborhoodAPI.Tests/SmartNeighborhoodAPI.Tests.csproj \
  --collect:"XPlat Code Coverage" \
  --results-directory ./TestResults
```

### Test Coverage Areas

| Test File | What It Covers |
|---|---|
| `IssueStatusHandlerFactoryTests.cs` | Correct handler resolution per `IssueStatus` enum value |
| `ResolvedIssueHandlerTests.cs` | Business logic of the `ResolvedIssueHandler` |
| `NotificationFactoryTests.cs` | Correct sender resolution per `NotificationChannel` |
| `ReportGeneratorFactoryTests.cs` | Correct generator resolution per `ReportFormat` |
| `PdfReportGeneratorTests.cs` | PDF generation output integrity |
| `DashboardServiceExportTests.cs` | Dashboard export pipeline with mocked dependencies |

---

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.
