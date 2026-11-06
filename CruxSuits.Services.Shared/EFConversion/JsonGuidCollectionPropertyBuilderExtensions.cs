using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Shared.EFConversion
{
    public static class JsonGuidCollectionPropertyBuilderExtensions
    {
        public static PropertyBuilder<ICollection<Guid>?> HasJsonGuidCollectionConversion(
            this PropertyBuilder<ICollection<Guid>?> propertyBuilder)
        {
            propertyBuilder.HasConversion(new JsonGuidCollectionConverter());
            propertyBuilder.Metadata.SetValueComparer(new JsonGuidCollectionComparer());
            propertyBuilder.HasColumnType("nvarchar(max)");

            return propertyBuilder;
        }
    }
}
