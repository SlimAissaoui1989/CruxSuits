# Identity JWT signing and discovery

Identity signs access tokens with an RSA private key. The corresponding public key is published as a JWKS document through the Gateway:

- `/.well-known/openid-configuration` at the Identity issuer URL
- `/.well-known/jwks.json` at the `jwks_uri` returned by the discovery document

The existing `POST /api/v1/Auth/login` contract is retained. Clients send the returned access token to protected services in the `Authorization: Bearer <token>` header.

The discovery document is used by the APIs to retrieve signing keys. This does not convert the existing custom login endpoint into a full OpenID Connect authorization-code flow.

## Service-to-service credentials

Identity also exposes a limited OAuth-style client credentials endpoint at `POST /api/v1/connect/token` (through the Gateway: `POST /identity/api/v1/connect/token`). A registered service sends `grant_type=client_credentials`, its client credentials, and the scopes it needs. Identity returns a five-minute RSA-signed access token. The secret is never returned in the token.

Register each calling service in Identity configuration using environment variables or a secret store. For example, to let the Files service call Person endpoints protected with the `ServiceToService` policy, configure:

| Environment variable | Example value |
|---|---|
| `ServiceClients__0__ClientId` | `files-service` |
| `ServiceClients__0__ClientSecret` | A long randomly generated secret stored outside source control |
| `ServiceClients__0__Scopes__0` | `person.api` |

The calling service requests the `person.api` scope. The Person API's `ServiceToService` policy requires both `token_use=service` and this scope. Use a separate client ID and secret for every service, and grant each client only the scopes required for its calls. Keep the Identity endpoint and all API endpoints on HTTPS in production.

The API policy is opt-in: add `[Authorize(Policy = "ServiceToService")]` only to endpoints intended for service callers. Existing user endpoints continue to use their normal `[Authorize]` rules. A policy does not itself block direct network access; production firewall or hosting rules must expose only the Gateway publicly and keep service ports private.

Example token request (use the Gateway's HTTPS URL in production):

```http
POST /identity/api/v1/connect/token HTTP/1.1
Host: localhost:5200
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials&client_id=files-service&client_secret=<secret>&scope=person.api
```

Then call a private service endpoint with `Authorization: Bearer <access_token>`.

## Local development

`appsettings.Development.json` configures the local issuer and audience. On first use, Identity generates a persistent RSA key at `.keys/jwt-signing-key.pem`. The `.keys` directory is ignored by Git; keep it in place while using tokens issued by that local Identity instance.

## Deployment configuration

Configure these values through environment variables, a secret store, or mounted secret files:

| Service | Configuration | Value |
|---|---|---|
| Identity | `Jwt__Issuer` | Stable public Gateway URL for Identity, such as `https://api.example.com/identity` |
| Identity | `Jwt__Audience` | `cruxsuits-api` |
| Identity | `Jwt__PrivateKeyPem` or `Jwt__PrivateKeyPath` | Persistent RSA private key; keep it only in Identity’s secret storage |
| Files, Person, ReferenceData | `Jwt__Authority` | The same stable Identity issuer URL |
| Files, Person, ReferenceData | `Jwt__Audience` | `cruxsuits-api` |

All Identity replicas must use the same signing private key. The Gateway must route `/identity/.well-known/{**remainder}` to the Identity service’s root-level discovery endpoints.
