
# TodoApi

TodoApi is a RESTful Web API built with **ASP.NET Core (.NET 9, C# 13)** for managing Todo tasks with user authentication, authorization, and auditing features.

## ✨ Features
- **User Authentication & Authorization**  
  - Secured with ASP.NET Core Identity and JWT Bearer tokens.
- **Todo Management**  
  - Full CRUD operations: create, update, delete, and list (with pagination support).
- **Audit Logging**  
  - Logs all actions (create, update, delete) with user details and IP address.
- **Rate Limiting**  
  - Restricts the number of requests per IP to prevent abuse.
- **Global Exception Handling**  
  - Centralized error handling with consistent API responses.
- **Swagger / OpenAPI**  
  - Interactive API documentation and testing via Swagger UI.

## 🛠️ Technologies Used
- ASP.NET Core 9  
- Entity Framework Core  
- SQL Server  
- ASP.NET Core Identity  
- JWT Authentication  
- Swagger / OpenAPI  

## 🚀 Getting Started
1. **Clone the repository**  

2. **Configure the connection string in appsettings.json**

3. **Run database migrations (if needed)**

4. **Build and run the project**
---