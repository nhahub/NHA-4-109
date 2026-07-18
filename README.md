# SMSRLY -- Property Recommendations Platform

## Overview

SMSRLY is a web-based property recommendation platform that connects
tenants directly with property owners while providing personalized
property recommendations through a machine learning service.

## Architecture

-   **Frontend:** React (Vite)
-   **Backend:** ASP.NET Core (.NET 8) using a Three-Tier Architecture
-   **Database:** SQL Server with Entity Framework Core
-   **Machine Learning:** Python recommendation service

### Backend Layers

    PresentationLayer/
    BusinessLogicLayer/
    DataAccessLayer/

## Features

-   JWT Authentication
-   Role-based Authorization (Admin, Owner, Tenant)
-   BCrypt Password Hashing
-   Property CRUD Operations
-   Machine Learning Recommendations
-   Swagger API Documentation
-   Entity Framework Core Migrations

## Technology Stack

-   ASP.NET Core 8
-   Entity Framework Core
-   SQL Server
-   React + Vite
-   Python (ML Service)
-   Redis
-   Docker

## Project Structure

    frontend/
    backend/
    ├── PresentationLayer/
    ├── BusinessLogicLayer/
    └── DataAccessLayer/
    ml/

## Getting Started

### 1. Configure

Create: - `appsettings.Development.json` - `jwtsettings.json` -
`corssettings.json` - `mlapisettings.json`

### 2. Apply Migrations

``` bash
dotnet ef database update --project ../DataAccessLayer --startup-project .
```

### 3. Run Backend

``` bash
dotnet run --project WebApplication1
```

Swagger: - https://localhost:7184/swagger

### 4. Run ML Service

``` bash
uvicorn app.main:app --reload --port 8000
```

### 5. Run Frontend

``` bash
npm install
npm run dev
```

## API

-   Authentication
-   Property Management
-   Recommendation
-   Admin ML Operations

Swagger provides interactive API documentation and JWT testing.

## Security

-   BCrypt password hashing
-   JWT authentication
-   Role-based authorization
-   Ownership authorization
-   Input validation

## License

This project was developed as an academic graduation project.
