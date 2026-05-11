# Sponsorship Request Creation, Drafts, Submit, Own Requests, Cancel

## Feature purpose
Allows requestors to create sponsorship requests, save them as Draft, submit them for manager approval, view their own requests, and cancel before approval.

## Related backend files
- `Application/DTOs/SponsorshipRequestDtos.cs`
- `Application/Services/SponsorshipRequestService.cs`
- `Controllers/RequestsController.cs`
- `Domain/Entities/SponsorshipRequest.cs`

## Related frontend files
- `frontend/src/pages/CreateRequestPage.tsx`
- `frontend/src/components/RequestForm.tsx`
- `frontend/src/pages/RequestListPage.tsx`
- `frontend/src/pages/RequestDetailPage.tsx`
- `frontend/src/api/requestsApi.ts`

## Database tables involved
`SponsorshipRequests`, `SponsorshipTypes`, `ApprovalHistories`, `Users`

## API endpoints involved
- `POST /api/requests`
- `PUT /api/requests/{id}`
- `POST /api/requests/{id}/submit`
- `GET /api/requests/mine`
- `POST /api/requests/{id}/cancel`

## Request example
```json
{
  "requestTitle": "AI Summit Sponsorship",
  "department": "Marketing",
  "sponsorshipTypeId": 1,
  "eventOrOrganisationName": "Regional AI Summit",
  "eventDate": "2026-08-20",
  "requestedAmount": 15000,
  "purposeJustification": "Brand visibility with enterprise technology buyers.",
  "expectedBusinessBenefit": "Pipeline generation and partner relationship growth.",
  "remarks": "Targeting gold tier sponsorship."
}
```

## Response example
Returns `SponsorshipRequestDetailDto` with status and audit history.

## Validation rules
Required title, department, type, event/organisation, event date, amount > 0, purpose >= 20 chars, business benefit >= 20 chars. Event date cannot be in the past.

## Role-based access rules
Only Requestor can create/update/submit/cancel. Requestors can only modify their own requests.

## Workflow/status transition rules
`Draft -> PendingManagerApproval`; `Draft/PendingManagerApproval -> Cancelled`.

## Error handling behavior
Invalid status or inactive sponsorship type returns 400 problem JSON. Missing request returns 404.

## Important implementation notes
Create always produces a Draft. Submit is a separate endpoint to keep save-draft and submit semantics clear.
