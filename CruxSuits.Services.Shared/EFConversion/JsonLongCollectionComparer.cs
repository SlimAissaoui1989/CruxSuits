using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CruxSuits.Services.Shared.EFConversion
{
    internal sealed class JsonLongCollectionComparer : ValueComparer<ICollection<long>?>
    {
        public JsonLongCollectionComparer()
            : base(
                (left, right) => left == right || (left != null && right != null && left.SequenceEqual(right)),
                value => value == null ? 0 : string.Join(",", value).GetHashCode(),
                value => value == null ? null : value.ToList())
        {
        }
    }
}
