using backend.Models.AssetModels;
using FluentValidation;

namespace backend.Validators
{
    public class AddressValidator : AbstractValidator<AssetAddress>
    {
        public AddressValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("העיר בכתובת הנכס הינה שדה חובה.");
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("הרחוב בכתובת הנכס הינה שדה חובה.");
            RuleFor(x => x.HouseNumber)
                .GreaterThan(0).When(x => x.HouseNumber.HasValue)
                .WithMessage("מספר הבית חייב להיות מספר חיובי.");
        }
    }
}
