# CruxSuits

CruxSuits is a .NET microservices application for identity, people and companies, uploaded files, and shared reference data. The repository includes a YARP API Gateway and shared libraries for common service and application code.

> This repository is under active development. Review the configuration and security notes before exposing a deployment publicly.

## Services

| Project | Responsibility |
|---|---|
| `CruxSuits.Services.Gateway` | Routes external HTTP requests to backend services with YARP. |
| `CruxSuits.Services.Identity` | User login, RSA-signed JWT access tokens, JWKS discovery, and service client credentials. |
| `CruxSuits.Services.Person` | People, companies, and their contact and identity information. |
| `CruxSuits.Services.Files` | Upload, download, update, and removal of files stored in SQL Server or on disk. |
| `CruxSuits.Services.ReferenceData` | Shared country, currency, language, and localized-name data. |
| `CruxSuits.Services.Shared` | Shared service defaults, EF Core converters, and common infrastructure. |
| `CruxSuits.Shared` | Shared application contracts and DTOs. |

The solution file is [`CruxSuits.slnx`](CruxSuits.slnx).

## Requirements

- .NET 10 SDK
- SQL Server for the Identity, Person, Files, and ReferenceData databases
- HTTPS development certificate for the Gateway's local HTTPS destinations

## Build

From the repository root:

```powershell
dotnet restore CruxSuits.slnx
dotnet build CruxSuits.slnx --configuration Release --no-restore
```

The GitHub Actions workflow builds the solution on pushes and pull requests. Add automated tests as test projects are introduced.

## Local configuration

Local `appsettings*.json` files are ignored because they may contain credentials. Provide configuration through environment variables, .NET user-secrets, or your deployment secret store. Do not commit database passwords, JWT private keys, service client secrets, or uploaded files.

Each data service needs its own `ConnectionStrings:DefaultConnection` value. For example, from PowerShell before starting one service:

```powershell
$env:ConnectionStrings__DefaultConnection = "<your SQL Server connection string>"
```

Set these JWT values for local development:

| Service | Configuration | Local value |
|---|---|---|
| Identity | `Jwt__Issuer` | `http://localhost:5200/identity` |
| Identity | `Jwt__Audience` | `cruxsuits-api` |
| Files, Person, ReferenceData | `Jwt__Authority` | `http://localhost:5200/identity` |
| Files, Person, ReferenceData | `Jwt__Audience` | `cruxsuits-api` |

Identity creates a persistent development signing key under `.keys/`. Keep that key in local development storage; production must use a managed secret or mounted key file and share the same signing key across Identity replicas.

## Run locally

Start Identity, Person, Files, ReferenceData, and then Gateway in separate terminals. From the repository root, for example:

```powershell
dotnet run --project CruxSuits.Services.Identity --launch-profile https
dotnet run --project CruxSuits.Services.Person --launch-profile https
dotnet run --project CruxSuits.Services.Files --launch-profile https
dotnet run --project CruxSuits.Services.ReferenceData --launch-profile https
dotnet run --project CruxSuits.Services.Gateway --launch-profile https
```

The Gateway listens at `https://localhost:7200` (and `http://localhost:5200`). Its root page reports status and route prefixes; `/health` reports gateway health. Trust the local ASP.NET Core development certificate if the HTTPS destinations are rejected.

The Gateway maps these public prefixes to backend services:

| Public prefix | Service path example |
|---|---|
| `/identity` | `/identity/Auth/login` |
| `/person` | `/person/{controller}/{action}` |
| `/files` | `/files/Files` |
| `/reference-data` | `/reference-data/{controller}/{action}` |

In Development, Identity publishes OpenID discovery at `/identity/.well-known/openid-configuration` and its public signing keys at `/identity/.well-known/jwks.json`. The login endpoint is `POST /identity/Auth/login`; the service client token endpoint is `POST /identity/connect/token`.

Files endpoints are available under `/files/Files`:

- `POST /files/Files` uploads multipart form data (`File`, `Description`, `IsPublic`, `IsSavedInDatabase`).
- `GET /files/Files/{id}/download` downloads a file.
- `PUT /files/Files/{id}` updates only `Description`, `IsPublic`, and `IsSavedInDatabase`.
- `DELETE /files/Files/{id}` removes the file.

For local OpenAPI and Scalar pages, run the corresponding API service directly; the Gateway does not proxy those development pages.

## Authentication and deployment

Identity signs access tokens with an RSA key and serves the public key through JWKS. APIs validate JWTs using the configured Identity authority and audience. Send access tokens with `Authorization: Bearer <token>`.

Identity also supports a client-credentials token endpoint for configured service clients. Keep each client secret in a secret store, grant only the scopes that client needs, and protect the corresponding routes with the `ServiceToService` authorization policy.

In production:

- Use HTTPS for public and service-to-service traffic.
- Expose the Gateway publicly and restrict backend service ports to a private network.
- Store connection strings, JWT private keys, and service client secrets outside the repository.
- Configure persistent file storage shared by the Files service instances that need to access the same files.
- Apply and review EF Core migrations for each database during deployment.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for branch, change, and pull request guidance. Report vulnerabilities privately as described in [SECURITY.md](SECURITY.md).

## License

This project is distributed under the MIT License. See [LICENSE](LICENSE).
