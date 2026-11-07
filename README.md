# MyWebApi – ASP.NET Core Web API

A modular and scalable REST API built using **ASP.NET Core 10, Entity Framework Core, SQL Server, CQRS, MediatR and JWT Authentication**.

The project follows a layered architecture to maintain separation of concerns, improve maintainability, and make the application easier to extend.

## 🚀 Tech Stack

- **Backend:** ASP.NET Core 10 Web API
- **Language:** C#
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server
- **Architecture:** Layered Architecture
- **Design Patterns:** CQRS, Repository Pattern, Dependency Injection
- **Mediator:** MediatR
- **Authentication:** JWT (JSON Web Token)
- **Password Security:** Password Hashing
- **API Documentation:** Swagger / OpenAPI
- **Containerization:** Docker Compose

## 📁 Project Structure

```text
MyWebApi
│
├── MyWebApi
│   ├── Controllers
│   ├── DTOs
│   ├── Properties
│   ├── Program.cs
│   └── appsettings.json
│
├── MyWebApi.Domain
│   ├── Entities
│   └── Interfaces
│
├── MyWebApi.Infrastructure
│   ├── Extensions
│   ├── Migrations
│   ├── Persistence
│   └── Repositories
│
├── MyWebApi.Services
│   ├── Command
│   ├── Constants
│   ├── Extensions
│   ├── Handler
│   ├── Interfaces
│   ├── Query
│   ├── Security
│   └── Validators
│
├── docker-compose.yml
└── MyWebApi.sln
```

## 🏗️ Architecture

The application follows a layered architecture.

### 1. MyWebApi (Presentation Layer)
- Exposes RESTful API endpoints.
- Handles HTTP requests and responses.
- Configures middleware and dependency injection.
- Configures JWT authentication and authorization.

### 2. MyWebApi.Domain (Domain Layer)
- Contains core business entities.
- Defines repository interfaces.
- Maintains application-independent domain contracts.

### 3. MyWebApi.Infrastructure (Infrastructure Layer)
- Implements repository interfaces.
- Manages database connectivity.
- Contains Entity Framework Core DbContext.
- Handles database migrations and entity configurations.

### 4. MyWebApi.Services (Application Layer)
- Implements business logic.
- Contains CQRS commands, queries and handlers.
- Uses MediatR for request handling.
- Handles validation and authentication-related services.
- Generates JWT tokens.

## ✨ Features

### Authentication & Authorization
- User Registration
- User Login
- JWT Token Generation
- JWT Token Validation
- Password Hashing and Verification
- Protected API Endpoints

### Database Management
- SQL Server Integration
- Entity Framework Core
- Code-First Approach
- Database Migrations
- Fluent API Entity Configurations
- Repository Pattern

### Application Architecture
- CQRS Pattern
- MediatR Integration
- Dependency Injection
- Separation of Concerns
- Centralized API Response Structure
- Input Validation

## ⚙️ Getting Started

### Prerequisites

Make sure you have installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Visual Studio Code or Visual Studio
- SQL Server
- Git

### 1. Clone the Repository

```bash
git clone <your-repository-url>

cd MyWebApi
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure SQL Server

Start SQL Server using Docker Compose:

```bash
docker compose up -d
```

Configure your database connection string in your development configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=MyWebApiDb;User Id=sa;Password=YOUR_DB_PASSWORD;TrustServerCertificate=True;"
  }
}
```

Replace the credentials and connection details according to your Docker configuration.

Do not commit real database credentials to Git.

### 4. Configure JWT

Initialize User Secrets:

```bash
dotnet user-secrets init --project MyWebApi
```

Configure your JWT secret key:

```bash
dotnet user-secrets set "Jwt:Key" "YOUR_LONG_RANDOM_SECRET_KEY" --project MyWebApi
```

Add the remaining JWT configuration to `appsettings.json`:

```json
{
  "Jwt": {
    "Issuer": "MyWebApi",
    "Audience": "MyWebApiUsers",
    "ExpiresInMinutes": 60
  }
}
```

### 5. Run EF Core Migrations

Install EF Core CLI if not already installed:

```bash
dotnet tool install --global dotnet-ef
```

Update the database:

```bash
dotnet ef database update \
  --project MyWebApi.Infrastructure \
  --startup-project MyWebApi
```

To create a new migration:

```bash
dotnet ef migrations add MigrationName \
  --project MyWebApi.Infrastructure \
  --startup-project MyWebApi
```

### 6. Build the Application

```bash
dotnet build
```

### 7. Run the Application

```bash
dotnet run --project MyWebApi
```

The API will start on the configured application URL.

Open Swagger to explore and test available endpoints.

## 🔐 Authentication Flow

The application uses JWT-based authentication.

1. User submits registration details.
2. Password is hashed before storing in SQL Server.
3. User submits email and password during login.
4. The application retrieves the user using the Repository Pattern.
5. Password is verified using the password hashing service.
6. A JWT token is generated after successful authentication.
7. The client sends the token in subsequent API requests.

Authorization header:

```http
Authorization: Bearer YOUR_JWT_TOKEN
```

Protected endpoints use the `[Authorize]` attribute.

## 📡 API Response Structure

The application follows a consistent generic API response structure.

Example:

```json
{
  "status": 200,
  "success": true,
  "message": "Login successful",
  "data": {
    "accessToken": "YOUR_JWT_TOKEN"
  }
}
```

## 🧩 Design Patterns Used

| Pattern | Purpose |
|---|---|
| CQRS | Separates read and write operations |
| Repository | Abstracts database operations |
| Dependency Injection | Manages service dependencies |
| Mediator | Decouples request handlers from controllers |
| Layered Architecture | Separates application responsibilities |

## 🔒 Security Practices

- Passwords are stored as hashes, never as plaintext.
- JWT tokens are digitally signed.
- JWT secret keys are stored using User Secrets during development.
- Protected endpoints require valid authentication.
- Database credentials should not be committed to source control.
- Sensitive information should not be included in JWT claims.

## 🛠️ Future Improvements

- Role-Based Authorization
- Refresh Token Mechanism
- Global Exception Handling Middleware
- Structured Logging
- Unit and Integration Testing
- Redis Caching
- Dockerized API Deployment
- Rate Limiting

## 👨‍💻 Author

**Kanishk Upadhyay**

Full Stack Developer | .NET | React | Angular | Node.js

---

⭐ If you find this project useful, feel free to star the repository!
