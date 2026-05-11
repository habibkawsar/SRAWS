# Workflow Audit History

## Feature purpose
Records every meaningful workflow event so admins and approvers can see who acted, when, what changed, and why.

## Related backend files
- `Domain/Entities/ApprovalHistory.cs`
- `Application/DTOs/AdminDtos.cs`
- `Application/Services/SponsorshipRequestService.cs`

## Related frontend files
- `frontend/src/pages/RequestDetailPage.tsx`

## Database tables involved
`ApprovalHistories`, `SponsorshipRequests`, `Users`, `Roles`

## API endpoints involved
`GET /api/requests/{id}` returns `approvalHistories`.

## Request/response examples
```json
{
  "id": 10,
  "fromStatus": "PendingFinanceReview",
  "toStatus": "Approved",
  "action": "FinanceApproved",
  "remarks": "Approved within quarterly budget.",
  "actionBy": "Farzana Finance",
  "actionByRole": "FinanceAdmin",
  "actionAt": "2026-05-11T09:20:00Z"
}
```

## Validation rules
Remarks max length is 1000. Reject actions require remarks.

## Role-based access rules
Requestors see histories for their own requests. Managers, FinanceAdmins, and SystemAdmins can inspect requests they are authorized to view.

## Workflow/status transition rules
History is created for create, draft update, submit, manager approve/reject, finance approve/reject, and cancel.

## Error handling behavior
History creation occurs inside the same DbContext save path as the request update.

## Important implementation notes
History stores enum values as strings for readability in PostgreSQL.
