using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Shared.EFConversion
{
    public static class JsonLongCollectionPropertyBuilderExtensions
    {
        public static PropertyBuilder<ICollection<long>?> HasJsonLongCollectionConversion(
            this PropertyBuilder<ICollection<long>?> propertyBuilder)
        {
            propertyBuilder.HasConversion(new JsonLongCollectionConverter());
            propertyBuilder.Metadata.SetValueComparer(new JsonLongCollectionComparer());
            propertyBuilder.HasColumnType("nvarchar(max)");

            return propertyBuilder;
        }
    }
}
