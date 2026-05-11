# SRAWS Sponsorship Request Approval Workflow System

SRAWS is a complete enterprise-style full-stack sponsorship request approval workflow system. It includes a .NET 8 Web API backend, PostgreSQL persistence with EF Core migrations, JWT authentication, strict role-based authorization, audit history, and a React + Vite + TypeScript frontend.

## Folder Structure

```text
SRAWS/
├── backend/
│   └── SponsorshipApproval.Api/
│       ├── Application/
│       │   ├── DTOs/
│       │   ├── Interfaces/
│       │   └── Services/
│       ├── Controllers/
│       ├── Domain/
│       │   ├── Entities/
│       │   └── Enums/
│       ├── Infrastructure/
│       │   ├── Data/
│       │   └── Migrations/
│       ├── Middleware/
│       ├── Security/
│       ├── Dockerfile
│       ├── Program.cs
│       └── SponsorshipApproval.Api.csproj
├── frontend/
│   ├── src/
│   │   ├── api/
│   │   ├── components/
│   │   ├── context/
│   │   ├── pages/
│   │   ├── routes/
│   │   ├── types/
│   │   └── styles.css
│   ├── firebase.json
│   ├── package.json
│   └── vite.config.ts
└── docs/
    ├── developer/
    └── user/
```

## Features

- JWT authentication with seeded demo users
- Role-based authorization for Requestor, Manager, FinanceAdmin, and SystemAdmin
- Sponsorship request draft creation, update, submit, cancel, and tracking
- Manager approval and rejection workflow
- Finance final approval and rejection workflow
- Strict workflow transitions
- Audit history for all key workflow events
- Sponsorship type administration
- Swagger API documentation
- PostgreSQL EF Core schema and migration
- React protected routes and role-aware UI
- Deployment-ready backend Dockerfile and Firebase Hosting config

## Tech Stack

| Layer | Technology |
| --- | --- |
| Backend | .NET 8 Web API, EF Core, JWT Bearer Auth, Swagger |
| Database | PostgreSQL |
| Frontend | React, Vite, TypeScript, React Router, Axios |
| Deployment | DigitalOcean App Platform, Firebase Hosting |

## Architecture Summary

The backend follows layered clean architecture principles inside one deployable Web API project. `Domain` contains entities and workflow enums. `Application` contains DTOs, interfaces, and service-layer business logic. `Infrastructure` contains EF Core persistence, migrations, and seed initialization. `Controllers` are thin HTTP adapters. Workflow transitions are centralized in `SponsorshipRequestService` to keep state changes consistent and auditable.

The frontend uses typed API clients, `AuthContext` for login state, protected routes for role enforcement, reusable components, and role-aware pages. Backend authorization remains the source of truth.

## Database Schema

Tables:

- `Roles`
- `Users`
- `SponsorshipTypes`
- `SponsorshipRequests`
- `ApprovalHistories`

Workflow statuses:

- `Draft`
- `PendingManagerApproval`
- `PendingFinanceReview`
- `Approved`
- `Rejected`
- `Cancelled`

Allowed transitions:

- `Draft -> PendingManagerApproval`
- `PendingManagerApproval -> PendingFinanceReview`
- `PendingManagerApproval -> Rejected`
- `PendingFinanceReview -> Approved`
- `PendingFinanceReview -> Rejected`
- `Draft/PendingManagerApproval -> Cancelled`

## API Endpoint Plan

| Endpoint | Method | Roles | Purpose |
| --- | --- | --- | --- |
| `/api/auth/login` | POST | Public | JWT login |
| `/api/requests` | POST | Requestor | Create draft |
| `/api/requests/{id}` | PUT | Requestor | Update draft |
| `/api/requests/{id}/submit` | POST | Requestor | Submit request |
| `/api/requests/mine` | GET | Requestor | View own requests |
| `/api/requests/{id}/cancel` | POST | Requestor | Cancel eligible request |
| `/api/requests/pending-manager` | GET | Manager, SystemAdmin | Manager queue |
| `/api/requests/{id}/manager/approve` | POST | Manager | Approve to finance |
| `/api/requests/{id}/manager/reject` | POST | Manager | Reject request |
| `/api/requests/pending-finance` | GET | FinanceAdmin, SystemAdmin | Finance queue |
| `/api/requests/{id}/finance/approve` | POST | FinanceAdmin | Final approve |
| `/api/requests/{id}/finance/reject` | POST | FinanceAdmin | Finance reject |
| `/api/requests/all` | GET | SystemAdmin | View all requests |
| `/api/requests/{id}` | GET | Authorized users | Request detail + audit history |
| `/api/sponsorship-types` | GET | Authenticated | List active types |
| `/api/sponsorship-types` | POST | SystemAdmin | Create type |
| `/api/sponsorship-types/{id}` | PUT | SystemAdmin | Update type |
| `/api/sponsorship-types/{id}` | DELETE | SystemAdmin | Delete/deactivate type |

## Setup Guide

### Prerequisites

- .NET 8 SDK
- Node.js 20+
- PostgreSQL 14+
- EF Core CLI: `dotnet tool install --global dotnet-ef`

### Backend

1. Create PostgreSQL database named `sraws`.
2. Copy `backend/SponsorshipApproval.Api/.env.example` values into your hosting environment or use `appsettings.Development.json` locally.
3. Run the API:

```bash
cd backend/SponsorshipApproval.Api
dotnet restore
dotnet ef database update
dotnet run
```

Swagger is available at `https://localhost:<port>/swagger` or `http://localhost:<port>/swagger` depending on your launch profile.

### Frontend

1. Configure API URL:

```bash
cd frontend
cp .env.example .env
```

2. Install and run:

```bash
npm install
npm run dev
```

Open `http://localhost:5173`.

## Test Accounts

| Role | Email | Password |
| --- | --- | --- |
| Requestor | `requestor@test.com` | `Password123!` |
| Manager | `manager@test.com` | `Password123!` |
| FinanceAdmin | `finance@test.com` | `Password123!` |
| SystemAdmin | `admin@test.com` | `Password123!` |

## Live URLs Placeholder

- Backend API: `https://your-digitalocean-app.ondigitalocean.app`
- Frontend: `https://your-firebase-site.web.app`

## Deployment

### DigitalOcean App Platform Backend

Use `backend/SponsorshipApproval.Api/Dockerfile`. Configure:

- `ConnectionStrings__DefaultConnection`
- `Jwt__Issuer`
- `Jwt__Audience`
- `Jwt__Key`
- `Jwt__ExpiresMinutes`
- `AllowedOrigins__0`
- `ASPNETCORE_URLS=http://+:8080`

### Firebase Hosting Frontend

```bash
cd frontend
npm run build
firebase deploy --only hosting
```

Set `VITE_API_BASE_URL` to your deployed backend before building.

## Documentation Index

### Developer Documentation

- [Authentication](docs/developer/01-authentication.md)
- [Role-Based Access Control](docs/developer/02-rbac.md)
- [Request Creation, Drafts, Submit, Own Requests, Cancel](docs/developer/03-request-creation.md)
- [Workflow Approval](docs/developer/04-workflow-approval.md)
- [Audit History](docs/developer/05-audit-history.md)
- [Admin Features](docs/developer/06-admin-features.md)
- [API Reference](docs/developer/07-api-reference.md)
- [Database Design](docs/developer/08-database-design.md)
- [Deployment](docs/developer/09-deployment.md)

### User Documentation

- [Getting Started](docs/user/01-getting-started.md)
- [Login Guide](docs/user/02-login-guide.md)
- [Requestor Guide](docs/user/03-requestor-guide.md)
- [Manager Guide](docs/user/04-manager-guide.md)
- [Finance Admin Guide](docs/user/05-finance-admin-guide.md)
- [System Admin Guide](docs/user/06-system-admin-guide.md)
- [Status Explanation](docs/user/07-status-explanation.md)
- [FAQ](docs/user/08-faq.md)

## Assessment Requirement Mapping Table

| Original requirement | Implemented feature | Related API | Related UI page | Documentation file |
| --- | --- | --- | --- | --- |
| JWT login | Authentication & Login | `POST /api/auth/login` | Login | `docs/developer/01-authentication.md`, `docs/user/02-login-guide.md` |
| Seed demo users | Database Migration & Seeding | Startup seeding | Login demo list | `docs/developer/08-database-design.md` |
| Role-based access control | RBAC | All protected endpoints | Protected routes/sidebar | `docs/developer/02-rbac.md` |
| Create sponsorship request | Request creation | `POST /api/requests` | Create Request | `docs/developer/03-request-creation.md`, `docs/user/03-requestor-guide.md` |
| Save draft | Draft workflow | `POST /api/requests`, `PUT /api/requests/{id}` | Create Request, Request Detail | `docs/developer/03-request-creation.md` |
| Submit request | Submit workflow | `POST /api/requests/{id}/submit` | Request Detail | `docs/developer/03-request-creation.md` |
| View own requests | Requestor list | `GET /api/requests/mine` | Requests | `docs/user/03-requestor-guide.md` |
| Cancel request | Cancel workflow | `POST /api/requests/{id}/cancel` | Request Detail | `docs/developer/03-request-creation.md` |
| Manager approval | Manager approve | `POST /api/requests/{id}/manager/approve` | Request Detail | `docs/developer/04-workflow-approval.md`, `docs/user/04-manager-guide.md` |
| Manager rejection | Manager reject | `POST /api/requests/{id}/manager/reject` | Request Detail | `docs/developer/04-workflow-approval.md` |
| Finance final approval | Finance approve | `POST /api/requests/{id}/finance/approve` | Request Detail | `docs/user/05-finance-admin-guide.md` |
| Finance rejection | Finance reject | `POST /api/requests/{id}/finance/reject` | Request Detail | `docs/developer/04-workflow-approval.md` |
| Status tracking | Status badges and details | `GET /api/requests/{id}` | Dashboard, Requests, Detail | `docs/user/07-status-explanation.md` |
| Workflow audit history | ApprovalHistories | `GET /api/requests/{id}` | Request Detail timeline | `docs/developer/05-audit-history.md` |
| Sponsorship type management | Admin type CRUD | `/api/sponsorship-types` | Sponsorship Types | `docs/developer/06-admin-features.md` |
| Admin view all requests | Admin all requests | `GET /api/requests/all` | Requests | `docs/user/06-system-admin-guide.md` |
| Dashboard | Role-aware dashboard metrics | Role-specific list APIs | Dashboard | `docs/user/01-getting-started.md` |
| API error handling | ProblemDetails middleware | All APIs | Alert banners | `docs/developer/07-api-reference.md` |
| Backend deployment | Docker/DigitalOcean ready | N/A | N/A | `docs/developer/09-deployment.md` |
| Frontend deployment | Firebase ready | N/A | N/A | `docs/developer/09-deployment.md` |
