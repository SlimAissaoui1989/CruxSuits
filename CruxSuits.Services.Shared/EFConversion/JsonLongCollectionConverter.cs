using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace CruxSuits.Services.Shared.EFConversion
{
    internal sealed class JsonLongCollectionConverter : ValueConverter<ICollection<long>?, string?>
    {
        public JsonLongCollectionConverter()
            : base(
                value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                json => JsonSerializer.Deserialize<ICollection<long>>(json ?? "null", (JsonSerializerOptions?)null))
        {
        }
    }
}
