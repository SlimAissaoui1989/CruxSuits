using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace CruxSuits.Services.Shared.EFConversion
{
    internal sealed class JsonGuidCollectionConverter : ValueConverter<ICollection<Guid>?, string?>
    {
        public JsonGuidCollectionConverter()
            : base(
                value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                json => JsonSerializer.Deserialize<ICollection<Guid>>(json ?? "null", (JsonSerializerOptions?)null))
        {
        }
    }
}
