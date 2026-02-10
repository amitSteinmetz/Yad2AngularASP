using backend.Models.AssetModels;
using FluentValidation;

namespace backend.Validators
{
    public class AssetFiltersValidator : AbstractValidator<AssetFilters>
    {
        public AssetFiltersValidator()
        {
            // --- מחיר ---
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("מחיר מינימלי לא יכול להיות שלילי.")
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).WithMessage("מחיר מקסימלי לא יכול להיות שלילי.")
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
                .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
                .WithMessage("המחיר המינימלי אינו יכול להיות גבוה מהמחיר המקסימלי.")
                .WithName("PriceRange");

            // --- חדרים ---
            RuleFor(x => x.MinRooms)
                .GreaterThanOrEqualTo(0).WithMessage("מספר חדרים מינימלי לא יכול להיות שלילי.")
                .When(x => x.MinRooms.HasValue);

            RuleFor(x => x.MaxRooms)
                .GreaterThanOrEqualTo(0).WithMessage("מספר חדרים מקסימלי לא יכול להיות שלילי.")
                .When(x => x.MaxRooms.HasValue);

            RuleFor(x => x)
                .Must(x => !x.MinRooms.HasValue || !x.MaxRooms.HasValue || x.MinRooms <= x.MaxRooms)
                .WithMessage("מספר החדרים המינימלי אינו יכול להיות גבוה ממספר החדרים המקסימלי.")
                .WithName("RoomsRange");

            // --- שטח כולל ---
            RuleFor(x => x.MinAreaInSquareMeters)
                .GreaterThanOrEqualTo(0).WithMessage("שטח מינימלי לא יכול להיות שלילי.")
                .When(x => x.MinAreaInSquareMeters.HasValue);

            RuleFor(x => x.MaxAreaInSquareMeters)
                .GreaterThanOrEqualTo(0).WithMessage("שטח מקסימלי לא יכול להיות שלילי.")
                .When(x => x.MaxAreaInSquareMeters.HasValue);

            RuleFor(x => x)
                .Must(x => !x.MinAreaInSquareMeters.HasValue || !x.MaxAreaInSquareMeters.HasValue || x.MinAreaInSquareMeters <= x.MaxAreaInSquareMeters)
                .WithMessage("השטח המינימלי אינו יכול להיות גבוה מהשטח המקסימלי.")
                .WithName("AreaRange");

            // --- שטח בנוי ---
            RuleFor(x => x.MinBuiltAreaInSquareMeters)
                .GreaterThanOrEqualTo(0).WithMessage("שטח בנוי מינימלי לא יכול להיות שלילי.")
                .When(x => x.MinBuiltAreaInSquareMeters.HasValue);

            RuleFor(x => x.MaxBuiltAreaInSquareMeters)
                .GreaterThanOrEqualTo(0).WithMessage("שטח בנוי מקסימלי לא יכול להיות שלילי.")
                .When(x => x.MaxBuiltAreaInSquareMeters.HasValue);

            RuleFor(x => x)
                .Must(x => !x.MinBuiltAreaInSquareMeters.HasValue || !x.MaxBuiltAreaInSquareMeters.HasValue || x.MinBuiltAreaInSquareMeters <= x.MaxBuiltAreaInSquareMeters)
                .WithMessage("השטח הבנוי המינימלי אינו יכול להיות גבוה מהשטח הבנוי המקסימלי.")
                .WithName("BuiltAreaRange");

            // --- דפדוף ---
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("מספר דף חייב להיות גדול מ-0.");

            RuleFor(x => x.PageSize).InclusiveBetween(1, 50); // הגנה מפני בקשות ענק

            // --- תקינות Enum ---
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("סוג הנכס הינו שדה חובה.")
                .IsInEnum().WithMessage("סוג הנכס שנבחר אינו תקין.")
                .When(x => x.Type.HasValue);

            RuleFor(x => x.Condition)
                .NotEmpty().WithMessage("מצב הנכס הינו שדה חובה.")
                .IsInEnum().WithMessage("מצב הנכס שנבחר אינו תקין.")
                .When(x => x.Condition.HasValue);
        }
    }
}