# API Reference

Base URL: `/api`

## Authentication
`POST /auth/login` public JWT login.

## Requests
`GET /requests/mine` Requestor own list.
`GET /requests/pending-manager` Manager/SystemAdmin pending manager queue.
`GET /requests/pending-finance` FinanceAdmin/SystemAdmin pending finance queue.
`GET /requests/all` SystemAdmin all requests.
`GET /requests/{id}` authorized request detail and audit history.
`POST /requests` Requestor create draft.
`PUT /requests/{id}` Requestor update draft.
`POST /requests/{id}/submit` Requestor submit draft.
`POST /requests/{id}/cancel` Requestor cancel Draft or PendingManagerApproval.
`POST /requests/{id}/manager/approve` Manager transition to PendingFinanceReview.
`POST /requests/{id}/manager/reject` Manager transition to Rejected.
`POST /requests/{id}/finance/approve` FinanceAdmin transition to Approved.
`POST /requests/{id}/finance/reject` FinanceAdmin transition to Rejected.

## Sponsorship Types
`GET /sponsorship-types` authenticated active list.
`GET /sponsorship-types?includeInactive=true` SystemAdmin full list.
`POST /sponsorship-types` SystemAdmin create.
`PUT /sponsorship-types/{id}` SystemAdmin update.
`DELETE /sponsorship-types/{id}` SystemAdmin delete/deactivate.

## Error format
Errors use RFC7807-style `ProblemDetails`.
```json
{ "status": 400, "title": "Business rule violation", "detail": "Request must be in Draft status.", "instance": "/api/requests/1/submit" }
```

## Swagger
Swagger is enabled at `/swagger` and includes Bearer JWT configuration.
