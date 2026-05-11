# Role-Based Access Control

## Feature purpose
Restricts API and UI operations to Requestor, Manager, FinanceAdmin, and SystemAdmin responsibilities.

## Related backend files
- `Program.cs`
- `Controllers/RequestsController.cs`
- `Controllers/SponsorshipTypesController.cs`
- `Controllers/ControllerExtensions.cs`

## Related frontend files
- `frontend/src/routes/ProtectedRoute.tsx`
- `frontend/src/components/Layout.tsx`
- `frontend/src/context/AuthContext.tsx`

## Database tables involved
`Users`, `Roles`

## API endpoints involved
All authenticated endpoints use `[Authorize]`; workflow actions use role-specific `[Authorize(Roles = "...")]` attributes.

## Request/response examples
Authorization header:
```http
Authorization: Bearer <jwt>
```

## Validation rules
JWT issuer, audience, signing key, and lifetime are validated.

## Role-based access rules
Requestor creates, views own, submits, and cancels eligible requests. Manager approves/rejects manager-pending requests. FinanceAdmin final approves/rejects finance-pending requests. SystemAdmin views all and manages sponsorship types.

## Workflow/status transition rules
RBAC is enforced before workflow service transition checks.

## Error handling behavior
Missing/invalid JWT returns framework 401. Forbidden business ownership checks return problem JSON.

## Important implementation notes
Frontend role rendering improves UX but backend authorization is the source of truth.
