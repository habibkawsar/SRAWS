# Database Design

## Tables
`Roles`: role lookup seeded with Requestor, Manager, FinanceAdmin, SystemAdmin.
`Users`: demo and future users, linked to one role.
`SponsorshipTypes`: active/inactive request categories.
`SponsorshipRequests`: core workflow entity and sponsorship fields.
`ApprovalHistories`: immutable audit trail of workflow actions.

## Relationships
One Role has many Users. One User has many SponsorshipRequests. One SponsorshipType has many SponsorshipRequests. One SponsorshipRequest has many ApprovalHistories. One User has many ApprovalHistories as actor.

## EF files
- `Infrastructure/Data/ApplicationDbContext.cs`
- `Infrastructure/Migrations/202605110001_InitialCreate.cs`
- `Infrastructure/Data/DbInitializer.cs`

## Migration and seeding
Run `dotnet ef database update --project backend/SponsorshipApproval.Api`. On app startup, `DbInitializer` applies migrations and seeds demo users if none exist.

## Indexes
Unique indexes exist for role name, user email, and sponsorship type name. Request status is indexed for queue views. Audit history is indexed by request and action date.
