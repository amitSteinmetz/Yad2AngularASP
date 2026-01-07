using backend.Models.Asset;
using FluentValidation;

namespace backend.Validators
{
    public class PublisherValidator : AbstractValidator<AssetPublisher>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("חובה להזין מספר מזהה של המפרסם");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("שם המפרסם הינו שדה חובה.")
                .MaximumLength(100).WithMessage("שם המפרסם לא יכול לעלות על 100 תווים.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("חובה להזין מספר טלפון")
                .Must(BeAValidIsraeliPhoneNumber)
                .WithMessage("מספר הטלפון חייב להיות מספר ישראלי תקין");
        }

        private bool BeAValidIsraeliPhoneNumber(string phoneNumber)
        {
            // 1. ניקוי: השארת ספרות בלבד (מסיר מקפים, רווחים, סוגריים)
            string cleaned = new([..phoneNumber.Where(char.IsDigit)]);

            // 2. בדיקה שהמספר מתחיל ב-0
            if (!cleaned.StartsWith('0')) return false;

            // 3. בדיקת אורך (9 לנייח, 10 לנייד)
            return cleaned.Length == 9 || cleaned.Length == 10;
        }
    }
}
