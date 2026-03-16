# Backend (ASP.NET Core MVC Web API)

This backend uses a standard Microsoft ASP.NET Core controller-based Web API structure.

## Run

```bash
cd backend
dotnet restore
dotnet run
```

Default URLs:

- http://localhost:5171
- https://localhost:7216

## Test Endpoint

- GET /api/weatherforecast

Example:

```bash
curl http://localhost:5171/api/weatherforecast
```
