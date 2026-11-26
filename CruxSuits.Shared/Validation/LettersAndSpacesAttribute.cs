using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.Validation;

public sealed class LettersAndSpacesAttribute : RegularExpressionAttribute
{
    public LettersAndSpacesAttribute()
        : base(@"\p{L}+(?: \p{L}+)*")
    {
        ErrorMessage = "The name must contain only letters separated by spaces.";
    }
}
