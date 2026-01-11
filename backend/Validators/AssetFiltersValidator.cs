using backend.Models.AssetModels;
using FluentValidation;

namespace backend.Validators
{
    public class AssetFiltersValidator : AbstractValidator<AssetFilters>
    {
        public AssetFiltersValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("MinPrice must be greater than or equal to 0.")
                .When(x => x.MinPrice.HasValue);
            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).WithMessage("MaxPrice must be greater than or equal to 0.")
                .When(x => x.MaxPrice.HasValue);
            RuleFor(x => x)
                .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
                .WithMessage("MinPrice cannot be greater than MaxPrice.");
            RuleFor(x => x.MinRooms)
                .GreaterThanOrEqualTo(0).WithMessage("MinRooms must be greater than or equal to 0.")
                .When(x => x.MinRooms.HasValue);
            RuleFor(x => x.MinAreaInSquareMeters)
                .GreaterThanOrEqualTo(0).WithMessage("MinAreaInSquareMeters must be greater than or equal to 0.")
                .When(x => x.MinAreaInSquareMeters.HasValue);
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");
        }
    }
}
