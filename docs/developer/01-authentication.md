# Authentication & Login

## Feature purpose
Provides JWT login for seeded demo users and returns user profile/role claims used by frontend route protection.

## Related backend files
- `backend/SponsorshipApproval.Api/Controllers/AuthController.cs`
- `backend/SponsorshipApproval.Api/Application/Services/AuthService.cs`
- `backend/SponsorshipApproval.Api/Security/JwtTokenService.cs`
- `backend/SponsorshipApproval.Api/Security/Pbkdf2PasswordHasher.cs`
- `backend/SponsorshipApproval.Api/Infrastructure/Data/DbInitializer.cs`

## Related frontend files
- `frontend/src/pages/LoginPage.tsx`
- `frontend/src/context/AuthContext.tsx`
- `frontend/src/api/authApi.ts`
- `frontend/src/api/http.ts`

## Database tables involved
`Users`, `Roles`

## API endpoints involved
`POST /api/auth/login`

## Request example
```json
{ "email": "requestor@test.com", "password": "Password123!" }
```

## Response example
```json
{
  "accessToken": "jwt-token",
  "expiresAtUtc": "2026-05-11T12:00:00Z",
  "user": { "id": 1, "fullName": "Rina Requestor", "email": "requestor@test.com", "department": "Marketing", "role": "Requestor" }
}
```

## Validation rules
Email is required and must be valid. Password is required and must be at least 8 characters.

## Role-based access rules
Login is public. Returned JWT contains `role`, `nameidentifier`, `email`, and `department` claims.

## Workflow/status transition rules
Authentication does not transition workflow status.

## Error handling behavior
Invalid credentials throw `UnauthorizedAccessException` and are formatted by middleware as `application/problem+json`.

## Important implementation notes
Passwords are hashed with PBKDF2-SHA256. Demo users are created at startup after EF migrations run.
