# TaskManagement API

A .NET-based Task Management API implementing CQRS, JWT authentication, and layered architecture.

---

## Architecture

The project follows a **layered clean architecture**:

- **Domain**: Core entities, value objects, and domain logic.  
- **Application**: CQRS commands, queries, and service handlers.  
- **Infrastructure**: Database context, repositories, JWT helpers, and external integrations.  
- **API / Presentation**: HTTP endpoints, request/response models, and dependency injection.

**Current Completion Status:**

| Layer            | Status          |
|-----------------|----------------|
| Domain           | ✅ Complete     |
| Application      | ✅ Complete     |
| Infrastructure   | ⚠ In Progress  |
| API / Presentation | ⚠ In Progress  |
| JWT Auth         | 🔹 80% Complete |

---

## Features

- User authentication with **JWT tokens**  
- CQRS-based command and query separation  
- PostgreSQL database support via Entity Framework Core  
- Dependency Injection using `IOptions<T>` for configuration  
- Token helpers with access & refresh token management  

---
Migration :
dotnet ef migrations add InitialCreate -p TaskManagement.Infrastructure\TaskManagement.Infrastructure.csproj -s TaskManagement.Api\TaskManagement.Api.csproj

dotnet ef database update -p TaskManagement.Infrastructure\TaskManagement.Infrastructure.csproj -s TaskManagement.Api\TaskManagement.Api.csproj


## Configuration

Add the following to `appsettings.json`:

```json
"Auth": {
  "Jwt": {
    "Issuer": "www.test.com",
    "Audience": "www.test.com",
    "AccessToken": {
      "ExpireInMinutes": 30,
      "SecretKey": "vtRap!QdYv%jsqpxU*tsceQ%bt2k768q2wH"
    },
    "RefreshToken": {
      "ExpireInMinutesIfNotRemember": 1440,
      "ExpireInMinutesIfRemember": 4320,
      "ClockSkewInMinutes": 60
    }
  }
}
