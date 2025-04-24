# Talabat E-Commerce APIs

A full-featured **E-Commerce RESTful API** built with **ASP.NET Core Web API**, following modern architectural and design patterns for scalability, performance, and maintainability. This solution includes both API endpoints and an MVC-based Admin Dashboard.

---

## 🔧 Features

- ✅ **Basket Functionality** with caching to prevent unnecessary data from being entered into the database.
- ✅ **Order Management** module for handling customer orders

- ✅ **Identity Authentication** with JWT
- ✅ **Role-based Authorization** (Admin/User)
- ✅ CRUD operations for:
  - Products
  - Categories
  - Brands
- ✅ **Admin Dashboard** using **ASP.NET Core MVC**
  - Manage users and roles
  - Manage products and categories
- ✅ **Image Upload Support** via Document Settings
- ✅ **Redis Caching** for optimized responses (using Filters)
- ✅ **Specification Pattern** for dynamic query building
- ✅ **Generic Repository** & **Unit of Work** design patterns
- ✅ **Onion Architecture** for modular and clean layering
- ✅ **AutoMapper** & **Extension Methods** for DTO mapping
- ✅ **Custom Middleware** for global exception handling
- ✅ **Data Seeding**: Auto-inserts initial data on first run
- ✅ **Automatic Migrations**: Applies EF Core migrations at startup

---

## 🏁 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/MohamedElsharif22/Talabat.Solution/
```

### 2. Register & Login

To test the API:

- Register with any email
- Use a password in the format: `P@ssw0rd`
- You may use `P@ssw0rd` directly for testing.

---

## 🏗️ Architecture Overview

This solution follows Clean Architecture (Onion Architecture), separated into:

- **API Layer** – Handles HTTP requests and responses
- **Application Layer** – Business logic and service contracts
- **Infrastructure Layer** – Data access, Redis caching, file uploads
- **Domain Layer** – Core entities and domain models
- **MVC Layer** – Admin dashboard for management tasks

---

## ⚙️ Technologies Used

- ASP.NET Core 7
- Entity Framework Core
- SQL Server
- Redis – For response caching
- JWT Authentication
- AutoMapper
- Swagger UI – For API testing
- MVC (Razor Views) – For the Admin Dashboard
- Postman / Insomnia – Optional API testing

---

## 📷 API Documentation

To access the Swagger documentation:

```
/swagger/index.html
```

> Replace `{port}` with your local port number (default is usually `5001` or `443`).

---

## 🧪 Testing Tools

You can use either of these tools to test endpoints manually:

- [Postman](https://www.postman.com/)
- [Insomnia](https://insomnia.rest/)

---

## 🤝 Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss your proposals.
