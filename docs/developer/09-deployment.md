# Deployment

## Backend: DigitalOcean App Platform
Build from `backend/SponsorshipApproval.Api`. Use the provided `Dockerfile`. Configure environment variables from `backend/SponsorshipApproval.Api/.env.example`.

Required variables:
- `ASPNETCORE_URLS=http://+:8080`
- `ConnectionStrings__DefaultConnection`
- `Jwt__Issuer`
- `Jwt__Audience`
- `Jwt__Key`
- `Jwt__ExpiresMinutes`
- `AllowedOrigins__0`

## Frontend: Firebase Hosting
Build from `frontend` with `npm run build`. Deploy `dist` using `firebase deploy --only hosting`. Configure `VITE_API_BASE_URL` before build.

## PostgreSQL
Use DigitalOcean Managed PostgreSQL or another PostgreSQL host. Ensure SSL settings in the connection string match the provider.

## Important implementation notes
The backend applies migrations at startup. For stricter enterprise deployments, run migrations as a release job before starting the web service.
