# Contributing to CruxSuits

Thank you for helping improve CruxSuits. Please keep changes focused and explain their impact in the pull request.

## Before making a change

1. Check open issues and pull requests for related work.
2. For larger changes, open an issue first to agree on the approach.
3. Create a short-lived branch from `microservices`, for example `feature/files-stream-download` or `fix/identity-token-validation`.

## Development

- Use the .NET 10 SDK and follow the repository `.editorconfig` settings.
- Keep service-specific code in its service project; put reusable service infrastructure in `CruxSuits.Services.Shared`.
- Do not commit secrets, local `appsettings*.json` files, signing keys, database files, uploads, or build output.
- Update DTOs, profiles, configuration, and API documentation when a public contract changes.
- Add or update automated tests when test projects are available for the affected behavior.
- Build the solution before opening a pull request:

  ```powershell
  dotnet build CruxSuits.slnx --configuration Release
  ```

## Pull requests

- Target the `microservices` branch for changes to this version.
- Explain the problem, the change, and any configuration or migration steps.
- Include screenshots only when a visual change needs them; for API work, include example requests and responses instead.
- Call out breaking API or database changes.
- Ensure generated or local files are not included in the diff.
