# Workflow Approval

## Feature purpose
Implements manager and finance approval/rejection with strict state transitions.

## Related backend files
- `Application/Services/SponsorshipRequestService.cs`
- `Controllers/RequestsController.cs`
- `Domain/Enums/WorkflowStatus.cs`
- `Domain/Enums/WorkflowAction.cs`

## Related frontend files
- `frontend/src/pages/RequestDetailPage.tsx`
- `frontend/src/pages/RequestListPage.tsx`
- `frontend/src/api/requestsApi.ts`

## Database tables involved
`SponsorshipRequests`, `ApprovalHistories`, `Users`

## API endpoints involved
- `GET /api/requests/pending-manager`
- `POST /api/requests/{id}/manager/approve`
- `POST /api/requests/{id}/manager/reject`
- `GET /api/requests/pending-finance`
- `POST /api/requests/{id}/finance/approve`
- `POST /api/requests/{id}/finance/reject`

## Request example
```json
{ "remarks": "Budget impact reviewed and approved." }
```

## Response example
Returns the updated request detail including a new audit history row.

## Validation rules
Reject endpoints require remarks. Approve endpoints may include optional remarks.

## Role-based access rules
Manager can approve/reject only `PendingManagerApproval`. FinanceAdmin can approve/reject only `PendingFinanceReview`.

## Workflow/status transition rules
`PendingManagerApproval -> PendingFinanceReview`, `PendingManagerApproval -> Rejected`, `PendingFinanceReview -> Approved`, `PendingFinanceReview -> Rejected`.

## Error handling behavior
Attempting invalid transitions returns 400 with current status in the message.

## Important implementation notes
All transitions are centralized in the service layer to avoid duplicate controller logic.
