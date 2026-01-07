using backend.Models.AssetModels;
using FluentValidation;

namespace backend.Validators
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            // המרה של ה-Annotations
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("סוג נכס לא תקין"); // ask gemini what it does

            RuleFor(x => x.Condition)
                .IsInEnum().WithMessage("מצב נכס לא תקין");

            RuleFor(x => x.Description)
                .MaximumLength(700).WithMessage("התיאור לא יכול לעלות על 700 תווים");

            RuleFor(x => x.Floor)
                .InclusiveBetween(-1, 100).WithMessage("קומה חייבת להיות בין 1- ל-100");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("המחיר לא יכול להיות שלילי");

            RuleFor(x => x.NumberOfRooms)
                .InclusiveBetween(1, 20).When(x => x.NumberOfRooms.HasValue)
                .WithMessage("מספר חדרים חייב להיות בין 1 ל-20");

            RuleFor(x => x.MainImageUrl)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _)) // ask gemini what it does
                .WithMessage("כתובת התמונה הראשית אינה תקינה");

            // 1. לוגיקה: שטח בנוי מול שטח כללי
            RuleFor(x => x.BuiltAreaInSquareMeters)
                .LessThanOrEqualTo(x => x.AreaInSquareMeters)
                .WithMessage("השטח הבנוי לא יכול להיות גדול מהשטח הכולל של הנכס");

            // 2. לוגיקה: תאריך כניסה מול תאריך פרסום
            RuleFor(x => x.EntryDate)
                .GreaterThanOrEqualTo(x => x.PublishDate) // ask gemini how it comapre dates
                .When(x => x.EntryDate.HasValue)
                .WithMessage("תאריך הכניסה חייב להיות היום או בעתיד");

            // ולידציה לאובייקטים מורכבים (Address & Publisher)
            RuleFor(x => x.Address).SetValidator(new AddressValidator()); // implement AddressValidator
            RuleFor(x => x.ContactDetails).SetValidator(new ContactDetailsValidator()); // implement PublisherValidator

            // ולידציה לרשימת התמונות (Gallery)
            RuleForEach(x => x.GalleryImageUrls)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("אחד הקישורים בגלריה אינו תקין");
        }
    }
}
