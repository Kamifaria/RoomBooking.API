# RoomBooking.API 🏢

A RESTful API for managing **meeting room reservations** in a company. This project was built with **.NET 8** following clean architecture principles to simulate a corporate room booking system.

---

## 📚 Project Overview

RoomBooking.API allows users to:

- ✍️ Register and manage users (employees)
- 📅 Schedule reservations for rooms
- ❌ Prevent schedule conflicts
- 🔒 Authenticate via JWT
- ⚖️ Assign event categories and types
- 🔍 Visualize and test endpoints via Swagger UI

---

## 🚀 Technologies Used

| Technology           | Purpose                                |
|----------------------|----------------------------------------|
| .NET 8               | API Framework                          |
| Entity Framework Core| ORM for database management            |
| SQL Server (LocalDB) | Development database                   |
| AutoMapper           | Mapping between Entities and DTOs      |
| Swagger (Swashbuckle)| API documentation                      |
| JWT Authentication   | Secure authentication and authorization|

---

## 📦 Project Structure

```
RoomBooking.API (Web layer)
RoomBooking.Application (Services and DTOs)
RoomBooking.Domain (Entities and Interfaces)
RoomBooking.Infrastructure.Data (DbContext, EF Migrations, Repositories)
RoomBooking.Exceptions (Custom Exceptions)
```

---

## ⚡ Features in Detail

- **User Management**: Create, read, update, and delete users
- **Room Management**: Create meeting rooms and define capacity
- **Reservation**: Schedule events with start/end times and related metadata
- **Validation**: Prevent overlapping bookings
- **Swagger UI**: Auto-generated API documentation
- **Dependency Injection**: Configured in IoC container

---

## 💼 How to Run Locally

### Ὄ1 Prerequisites:
- [.NET SDK 8+](https://dotnet.microsoft.com/en-us/download)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

### 🔧 Run the Project
```bash
# Clone the repo
git clone https://github.com/Kamifaria/RoomBooking.API.git
cd RoomBooking.API

# Restore dependencies
dotnet restore

# Apply migration and create the DB
dotnet ef database update -s RoomBooking.API -p RoomBooking.Infrastructure.Data

# Run the API
dotnet run --project RoomBooking.API
```

### 🔍 Access Swagger:
```
https://localhost:5001/swagger
```

---

## 🚪 Authentication

- All protected routes require a **JWT token**.
- Use `/api/auth/login` with a valid user to receive your token.
- Send the token via `Authorization: Bearer {your_token}` header.

---

## 💡 Future Improvements

- [ ] Add FluentValidation to improve input validation
- [ ] Add unit tests with xUnit and Moq
- [ ] Create UI with Blazor or React
- [ ] Integrate with email notifications for reservation reminders

---

## 👩‍💻 Author

**Kamila Faria**

- Junior .NET Developer
- Passionate about backend development and clean code
- [LinkedIn](https://www.linkedin.com/in/seu-perfil)

---

## ✨ License

This project is licensed for personal and academic use only. Fork and adapt it to your learning journey! 

---

> 🚀 "Turning ideas into APIs, one controller at a time."
