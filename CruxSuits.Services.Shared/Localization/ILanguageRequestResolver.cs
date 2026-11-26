using Microsoft.AspNetCore.Http;

namespace CruxSuits.Services.Shared.Localization;

public interface ILanguageRequestResolver
{
    Task<string> ResolveAsync(HttpRequest request, CancellationToken cancellationToken = default);
}
