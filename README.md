<h1 align="center">🏗️ Final Touch</h1>
<p align="center">
  <b>All-in-One E-Commerce Platform for Finishing Materials</b><br>
  🎨 Buy. 📏 Calculate. 👷‍♂️ Hire.
</p>

<p align="center">
  <a href="#"><img alt=".NET 8" src="https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet" /></a>
  <a href="#"><img alt="Angular 18" src="https://img.shields.io/badge/Angular-18-red?logo=angular" /></a>
  <a href="#"><img alt="Clean Architecture" src="https://img.shields.io/badge/Architecture-Clean-brightgreen" /></a>
  <a href="#"><img alt="Stripe" src="https://img.shields.io/badge/Payments-Stripe-blue?logo=stripe" /></a>
  <a href="#"><img alt="Redis" src="https://img.shields.io/badge/Cache-Redis-darkred?logo=redis" /></a>
</p>

---

## 🌟 Overview

**FinalTouch** is a modern full-stack e-commerce solution for the finishing materials industry. It empowers customers to:

- 🛍️ Browse and purchase tiles, doors, paints, and more  
- 📐 Auto-calculate material needs based on unit area  
- 👷 Book professional technicians for installation  
- 💳 Pay securely via Stripe

Designed with scalability, clean architecture, and performance in mind — powered by the latest in .NET Core & Angular.

---

## 🚀 Live Demo

> 🎯 **Link**: Working on it

---

## ⚙️ Tech Stack

| Category         | Technology |
|------------------|------------|
| 👨‍💻 Frontend       | Angular 18 (Standalone, Lazy Loading, Reactive Forms) |
| 🧠 Backend        | .NET 8 Web API |
| 🧱 Architecture   | Clean Architecture (API / Core / Infrastructure) |
| 🧩 Patterns       | CQRS, Specification Pattern, Repository & UoW |
| 🔐 Auth           | JWT + ASP.NET Identity |
| 📦 Caching        | Redis |
| 💳 Payments       | Stripe API |
| 🧪 Validation     | FluentValidation |
| 📐 UI Framework   | Angular Material + Tailwind CSS |

---

## ✨ Key Features

- 🛒 **E-Commerce Engine**  
  Product listing, filtering, sorting, details, cart, and checkout

- 📐 **Smart Material Estimator**  
  Automatically calculates required quantity based on room size

- 👷 **Technician Booking**  
  Find and schedule professionals for installation work

- 🔄 **Modular Architecture**  
  Clean separation of concerns using CQRS & layered design

- 🔐 **Secure Authentication**  
  JWT-based auth with role management

- 💳 **Stripe Integration**  
  Real-time 3D Secure payment workflow

- 🔥 **Optimized Performance**  
  Redis-powered caching for faster data access

- 📊 **Admin Ready**  
  (Optional) Add dashboards for managing orders and products

---

## 👨‍💻 FinalTouch Dev Crew

Ziad, Mohsen, Abdullah and Abo-Saood

---

## ⚙️ Local Setup

### Backend (.NET API)
```bash
cd FinalTouch.ServerSide
dotnet restore
dotnet dev-certs https --trust
dotnet run
```
### Frontend (Angular)
```bash
cd final-touch-client
npm install
ng serve --proxy-config proxy.conf.json
