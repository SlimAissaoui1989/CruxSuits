using System.Globalization;
using System.Net.Http.Headers;
using CruxSuits.Shared.Constants;
using Microsoft.AspNetCore.Http;

namespace CruxSuits.Services.Shared.Localization;

public static class AcceptLanguageResolver
{
    public static string? Resolve(
        HttpRequest request,
        IReadOnlyCollection<string> supportedLanguageIds,
        string defaultLanguageId)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(supportedLanguageIds);
        ArgumentException.ThrowIfNullOrWhiteSpace(defaultLanguageId);

        var supportedLanguageOrder = supportedLanguageIds
            .Where(languageId => !string.IsNullOrWhiteSpace(languageId))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var supportedLanguages = supportedLanguageOrder.ToHashSet(StringComparer.OrdinalIgnoreCase);

        var preferences = new List<(string LanguageTag, double Quality, int Order)>();
        var order = 0;

        foreach (var headerValue in request.Headers[HttpHeaderNames.AcceptLanguage])
        {
            if (string.IsNullOrWhiteSpace(headerValue))
                continue;

            foreach (var value in headerValue.Split(','))
            {
                if (StringWithQualityHeaderValue.TryParse(value.Trim(), out var preference)
                    && preference is not null)
                {
                    preferences.Add((preference.Value, preference.Quality ?? 1d, order));
                }

                order++;
            }
        }

        var explicitlyExcludedLanguageIds = preferences
            .Where(preference => preference.Quality <= 0d)
            .Select(preference => GetBaseLanguageId(preference.LanguageTag))
            .Where(languageId => languageId is not null && supportedLanguages.Contains(languageId))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (preferences.Any(preference => preference.LanguageTag == "*" && preference.Quality <= 0d))
            explicitlyExcludedLanguageIds.UnionWith(supportedLanguageOrder);

        var matches = new List<(string LanguageId, double Quality, int Order, int Specificity)>();
        foreach (var preference in preferences.Where(preference => preference.Quality > 0d))
        {
            if (preference.LanguageTag == "*")
            {
                for (var supportedLanguageIndex = 0; supportedLanguageIndex < supportedLanguageOrder.Length; supportedLanguageIndex++)
                {
                    var languageId = supportedLanguageOrder[supportedLanguageIndex];
                    if (!explicitlyExcludedLanguageIds.Contains(languageId))
                        matches.Add((languageId, preference.Quality, preference.Order, supportedLanguageIndex));
                }

                continue;
            }

            var preferredLanguageId = GetBaseLanguageId(preference.LanguageTag);
            if (preferredLanguageId is not null && supportedLanguages.Contains(preferredLanguageId))
                matches.Add((preferredLanguageId, preference.Quality, preference.Order, 0));
        }

        var matchedLanguageId = matches
            .OrderByDescending(match => match.Quality)
            .ThenBy(match => match.Order)
            .ThenBy(match => match.Specificity)
            .Select(match => match.LanguageId)
            .FirstOrDefault();

        if (matchedLanguageId is not null)
            return matchedLanguageId;

        if (preferences.Count == 0 || !preferences.Any(preference => preference.Quality <= 0d))
            return defaultLanguageId;

        if (supportedLanguages.Contains(defaultLanguageId)
            && !explicitlyExcludedLanguageIds.Contains(defaultLanguageId))
        {
            return defaultLanguageId;
        }

        return supportedLanguageOrder.FirstOrDefault(languageId => !explicitlyExcludedLanguageIds.Contains(languageId));
    }

    private static string? GetBaseLanguageId(string languageTag)
    {
        if (string.IsNullOrWhiteSpace(languageTag) || languageTag == "*")
            return null;

        var separatorIndex = languageTag.IndexOf('-');
        var baseLanguageId = separatorIndex >= 0
            ? languageTag[..separatorIndex]
            : languageTag;

        return CultureInfo.InvariantCulture.TextInfo.ToLower(baseLanguageId);
    }
}
