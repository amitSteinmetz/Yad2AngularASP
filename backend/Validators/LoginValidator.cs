using backend.DTOs;
using FluentValidation;

namespace backend.Validators
{
    public class LoginValidator : AbstractValidator<LoginDetails>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("אימייל הוא שדה חובה")
                .EmailAddress().WithMessage("פורמט האימייל אינו תקין");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("סיסמה היא שדה חובה")
                .Length(8, 16).WithMessage("הסיסמה חייבת להיות בין 8 ל-16 תווים")
                .Matches(@"[A-Z]").WithMessage("הסיסמה חייבת לכלול לפחות אות גדולה אחת")
                .Matches(@"[a-z]").WithMessage("הסיסמה חייבת לכלול לפחות אות קטנה אחת")
                .Matches(@"[0-9]").WithMessage("הסיסמה חייבת לכלול לפחות מספר אחד")
                .Matches(@"[@$!%*?&]").WithMessage("הסיסמה חייבת לכלול לפחות תו מיוחד אחד");
        }
    }
}
