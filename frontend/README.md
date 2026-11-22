# ECommerce

A full stack e-commerce application built with ASP.NET Core 9 Web API and React (TypeScript).

## Features

- Register / login with JWT authentication and role-based authorization (Admin / User)
- Product and category management (Admin only), category filtering on the storefront
- Server-side shopping cart (add, update quantity, remove)
- Checkout: price freezing, stock deduction, and cart clearing in a single transaction
- Order history

## Tech Stack

**Backend:** .NET 9, Entity Framework Core, SQL Server, ASP.NET Identity, JWT, Swagger
**Frontend:** React, TypeScript, Vite, Tailwind CSS, React Router, Axios

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server (local instance)
- Node.js 20+

### Backend

1. Create `api/appsettings.Development.json` with your connection string and a JWT signing key (at least 64 characters):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=EcommerceDb;Trusted_Connection=True;TrustServerCertificate=True"
     },
     "JWT": {
       "SigningKey": "your-long-random-key-at-least-64-characters"
     }
   }
   ```
2. `cd api`
3. `dotnet ef database update`
4. `dotnet run`
5. Swagger UI: `http://localhost:5246/swagger`

### Frontend

1. `cd
