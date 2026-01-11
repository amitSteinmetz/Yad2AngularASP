using backend.DTOs;
using FluentValidation;

namespace backend.Validators
{
    public class AssetDtoValidator : AbstractValidator<AssetDto>
    {
        public AssetDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("סוג הנכס הינו שדה חובה.")
                .IsInEnum().WithMessage("סוג נכס לא תקין");

            RuleFor(x => x.Condition)
                .NotEmpty().WithMessage("מצב הנכס הינו שדה חובה.")
                .IsInEnum().WithMessage("מצב נכס לא תקין");

            RuleFor(x => x.Description)
                .MaximumLength(700).WithMessage("התיאור לא יכול לעלות על 700 תווים");

            RuleFor(x => x.Floor)
                .InclusiveBetween(-1, 100).WithMessage("קומה חייבת להיות בין 1- ל-100");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("יש לציין מחיר");

            RuleFor(x => x.AreaInSquareMeters)
                .GreaterThan(0).WithMessage("שטח הנכס חייב להיות גדול מ-0");

            RuleFor(x => x.BuiltAreaInSquareMeters)
                .GreaterThanOrEqualTo(0).WithMessage("שטח בנוי לא יכול להיות שלילי")
                .LessThanOrEqualTo(x => x.AreaInSquareMeters)
                .WithMessage("השטח הבנוי לא יכול להיות גדול מהשטח הכולל");

            RuleFor(x => x.NumberOfRooms)
                .InclusiveBetween(1, 20).When(x => x.NumberOfRooms.HasValue)
                .WithMessage("מספר חדרים חייב להיות בין 1 ל-20");

            RuleFor(x => x.MainImageUrl)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("כתובת התמונה הראשית אינה תקינה");

            RuleFor(x => x.EntryDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .When(x => x.EntryDate.HasValue)
                .WithMessage("תאריך הכניסה חייב להיות היום או בעתיד");

            // קריאה לולידטורים של אובייקטים מורכבים
            RuleFor(x => x.Address).SetValidator(new AddressValidator());
            RuleFor(x => x.ContactDetails).SetValidator(new ContactDetailsValidator());

            RuleForEach(x => x.GalleryImageUrls)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("אחד הקישורים בגלריה אינו תקין");
        }
    }
}