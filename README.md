# SMSRLY -- Property Recommendation Platform

## Overview

SMSRLY is a web-based property recommendation platform that connects
tenants directly with property owners while providing personalized
property recommendations through a custom-made machine learning service as
the recommendations provider.

---

## Architecture

-   **Frontend:** React (Vite)
-   **Backend:** ASP.NET Core (.NET 8) using a Three-Tier Architecture
-   **Database:** SQL Server with Entity Framework Core
-   **Machine Learning:** Python recommendation service

### Backend Layers

    PresentationLayer/
    BusinessLogicLayer/
    DataAccessLayer/

---

## Features

-   JWT Authentication
-   Role-based Authorization (Admin, Owner, Tenant)
-   BCrypt Password Hashing
-   Property CRUD Operations
-   Machine Learning Recommendations
-   Swagger API Documentation
-   Entity Framework Core Migrations

---

## Technology Stack

-   ASP.NET Core 8
-   Entity Framework Core
-   SQL Server
-   React + Vite
-   Python (ML Service)
-   Redis
-   Docker

---

## Project Structure

    frontend/
    backend/
    ├── PresentationLayer/
    ├── BusinessLogicLayer/
    └── DataAccessLayer/
    ml/

---    

## Getting Started (local deployment)

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

Swagger: - https://localhost:5089/swagger

### 4. Run ML Service

``` bash
uvicorn app.main:app --reload --port 8000
```

### 5. Run Frontend

``` bash
npm install
npm run dev
```

---

## Containerized Deployment (Docker/Podman)

### Building


```bash
docker compose up --build -d
```

### Starting


```bash
docker compose start
```

### Deployment Details

- One-command deployment using Docker/Podman
- Only frontend exposed through its port
- Reverse proxy (Nginx) used to proxy backend API calls to frontend

---

## API

-   Authentication
-   Property Management
-   Recommendation
-   Admin ML Operations

Swagger provides interactive API documentation and JWT testing.

---

## Security

-   BCrypt password hashing
-   JWT authentication
-   Role-based authorization
-   Ownership authorization
-   Input validation

---

## License

This project was developed as an academic graduation project, with permission and supervision from [Digital Egypt Pioneers Program (DEPI)](http://depi.gov.eg/), and licensed under the ِApache 2.0 License.

```
Copyright 2026 Abdelrhman Elbrens

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    https://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
