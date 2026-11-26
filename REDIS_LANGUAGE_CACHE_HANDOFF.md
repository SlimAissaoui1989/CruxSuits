# Redis language catalog handoff

**Purpose:** first Redis slice for the language catalog. ReferenceData remains the database source of truth; the cache holds a shared copy for services to read.

## What changed

- The resolver now checks supported language IDs from the catalog instead of a hard-coded `en`, `fr`, `ar` list. The fallback language remains `Localization:DefaultLanguageId` in `appsettings.json` (`en`).
- The catalog is stored as one serialized snapshot at `cruxsuits:refdata:v1:languages` in Redis. Each process keeps a two-minute RAM copy to reduce repeated Redis reads.
- Successful language create/update/delete operations refresh the catalog after the database save. A ReferenceData background worker primes it and retries every two minutes.
- Redis registration is opt-in for ReferenceData in this slice; other services do not require Redis until they start consuming the cache.

## Files added

- `CruxSuits.Services.Shared/Localization/ILanguageCatalogCache.cs`
- `CruxSuits.Services.Shared/Localization/LanguageCatalogCache.cs`
- `CruxSuits.Services.Shared/Localization/LanguageCatalogCacheOptions.cs`
- `CruxSuits.Services.Shared/Localization/LanguageCatalogCacheServiceExtensions.cs`
- `CruxSuits.Services.ReferenceDatas/Localization/ILanguageCatalogProvider.cs`
- `CruxSuits.Services.ReferenceDatas/Localization/LanguageCatalogProvider.cs`
- `CruxSuits.Services.ReferenceDatas/Localization/LanguageCatalogRefreshWorker.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.UnitTests/Services/LanguageCatalogCacheTests.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.UnitTests/Services/LanguageCatalogProviderTests.cs`

## Files updated

- `CruxSuits.Services.Shared/CruxSuits.Services.Shared.csproj`
- `CruxSuits.Services.ReferenceDatas/Program.cs`
- `CruxSuits.Services.ReferenceDatas/appsettings.json`
- `CruxSuits.Services.ReferenceDatas/Localization/AcceptLanguageResolver.cs`
- `CruxSuits.Services.ReferenceDatas/Controllers/CountriesController.cs`
- `CruxSuits.Services.ReferenceDatas/Controllers/CurrenciesController.cs`
- `CruxSuits.Services.ReferenceDatas/Services/LanguageService.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.UnitTests/Services/AcceptLanguageResolverTests.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.UnitTests/Services/LanguageServiceTests.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.UnitTests/Controllers/CountriesControllerTests.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.UnitTests/Controllers/CurrenciesControllerTests.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.IntegrationTests/LanguagesApiIntegrationTests.cs`
- `CruxSuits.Services.Tests.ReferenceDatas.IntegrationTests/CountriesApiIntegrationTests.cs`

**Deleted:** none in this cache slice. Other pre-existing workspace changes were preserved.

## Checks and remaining setup

- ReferenceData unit tests: **121/121 passed**.
- ReferenceData integration tests: **44/44 passed**.
- Release solution build with warnings as errors: **passed, 0 warnings/errors**.
- Live Redis connectivity was not tested. Configure `ConnectionStrings:Redis` for ReferenceData outside Development/Testing and run a Redis instance. Development/Testing use a process-local substitute, which is not shared across services.
- The broader BDD run was not verified because the local SQL Server connection hit a TLS issue and Currency Reading steps are missing.

## Next time

1. Configure and run Redis, then verify ReferenceData can publish and read the catalog from it.
2. Add Redis consumers in other services when needed. The Persons identity-type catalog is a separate next slice because its records and translated labels are owned by different services.
