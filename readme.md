# PizzaSalesApi

PizzaSalesApi is a RESTful API for managing pizza sales, built with ASP.NET Core 9 and Entity Framework Core.

## Features

- Manage pizzas, pizza types, orders, and order details
- SQLite and SQL Server database support
- Swagger/OpenAPI documentation

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Setup

1. **Clone the repository:**
   ```sh
   git clone <your-repo-url>
   cd PizzaSalesAPI/PizzaSalesApi
   ```

2. **Restore dependencies:**
   ```sh
   dotnet restore
   ```

3. **Apply migrations (if needed):**
   ```sh
   dotnet ef database update
   ```

4. **Run the API:**
   ```sh
   dotnet run
   ```

5. **Access Swagger UI:**
   - Navigate to `http://localhost:5000/swagger` (or the port shown in the console)

### Configuration

- Edit `appsettings.json` or `appsettings.Development.json` to configure your database connection.

## Project Structure

- `Controllers/` - API controllers for pizzas, orders, etc.
- `Models/` - Entity models
- `Migrations/` - Entity Framework Core migrations

## Dependencies

- Microsoft.AspNetCore.OpenApi
- Microsoft.EntityFrameworkCore (Design, Relational, Sqlite, SqlServer, Tools)
- Swashbuckle.AspNetCore (Swagger)
- Microsoft.VisualStudio.Web.CodeGeneration.Design