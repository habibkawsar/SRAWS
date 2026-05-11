# Admin Features

## Feature purpose
SystemAdmin can view all requests and manage sponsorship types used by requestors.

## Related backend files
- `Controllers/RequestsController.cs`
- `Controllers/SponsorshipTypesController.cs`
- `Application/Services/SponsorshipTypeService.cs`

## Related frontend files
- `frontend/src/pages/AdminSponsorshipTypesPage.tsx`
- `frontend/src/pages/RequestListPage.tsx`
- `frontend/src/components/Layout.tsx`

## Database tables involved
`SponsorshipTypes`, `SponsorshipRequests`, `ApprovalHistories`

## API endpoints involved
- `GET /api/requests/all`
- `GET /api/sponsorship-types?includeInactive=true`
- `POST /api/sponsorship-types`
- `PUT /api/sponsorship-types/{id}`
- `DELETE /api/sponsorship-types/{id}`

## Request example
```json
{ "name": "Executive Roundtable", "description": "Invite-only customer leadership event", "isActive": true }
```

## Response example
```json
{ "id": 4, "name": "Executive Roundtable", "description": "Invite-only customer leadership event", "isActive": true }
```

## Validation rules
Name is required and unique. Description max length is 500.

## Role-based access rules
Only SystemAdmin can create, update, delete, include inactive types, and view all requests.

## Workflow/status transition rules
Admin views do not change request status.

## Error handling behavior
Deleting an in-use sponsorship type deactivates it instead of breaking relational history.

## Important implementation notes
Admin management preserves historical request references by using soft-deactivation when needed.
