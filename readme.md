# PizzaSales

PizzaSales is a full-stack solution for managing pizza sales, featuring a RESTful API built with ASP.NET Core 9 and Entity Framework Core, and a modern frontend built with Angular.

---

## Features

### Backend (ASP.NET Core API)
- Manage pizzas, pizza types, orders, and order details
- Import data from CSV files
- SQLite database support
- Swagger/OpenAPI documentation
- Dependency injection and modular service structure

### Frontend (Angular)
- Modern, responsive UI for managing pizza sales
- View, add, edit, and delete pizzas, pizza types, and orders
- Import data via CSV upload
- HTTP client integration with the backend API
- Routing, forms, and validation

---

## Prerequisites

### Backend
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Frontend
- [Node.js (LTS)](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)

---

## Getting Started

### Backend Setup

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
   - API will be running in `http://localhost:5264`.
   - It is important for the API to run in this address to communicate with the Angular app.

5. **Access Swagger UI:**
   - Navigate to `http://localhost:5264/swagger` (or the port shown in the console)

---

### Frontend Setup

1. **Navigate to the Angular app folder:**
   ```sh
   cd pizza-sales-app
   ```

2. **Install dependencies:**
   ```sh
   npm install
   ```

3. **Run the Angular app:**
   ```sh
   npm start
   ```
   - The app will be available at `http://localhost:5001/`.
   - It is import for the Angular project to run at this port address to communicate with the API.

4. **Configure API endpoint:**
   - Update the API base URL in your Angular environment files (e.g., `src/environments/environment.ts`) if needed.

---

## Project Structure

### Backend
- `Controllers/` - API controllers for pizzas, orders, etc.
- `Models/Entities/` - Entity Framework Core models
- `Models/DTOs/` - Data Transfer Objects for API requests/responses
- `Services/` - Business logic and data import services
- `Migrations/` - Entity Framework Core migrations

### Frontend
- `pizza-sales-app/src/app/` - Angular application source code
  - `components/` - UI components
  - `services/` - Angular services for API communication
  - `models/` - TypeScript interfaces and models
  - `environments/` - Environment configuration files

---

## Dependencies

### Backend
- Microsoft.AspNetCore.OpenApi
- Microsoft.EntityFrameworkCore (Design, Relational, Sqlite, SqlServer, Tools)
- Swashbuckle.AspNetCore (Swagger)
- CsvHelper
- Microsoft.VisualStudio.Web.CodeGeneration.Design

### Frontend
- Angular (latest stable)
- Angular Material (optional, for UI components)
- RxJS
- Other dependencies as listed in `package.json`