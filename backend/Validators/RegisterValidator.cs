using backend.DTOs;
using FluentValidation;

namespace backend.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDetails>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("חובה להזין שם פרטי")
                .MinimumLength(2).WithMessage("שם פרטי חייב להכיל לפחות 2 תווים")
                .MaximumLength(50).WithMessage("שם פרטי לא יכול לעלות על 50 תווים")
                .Matches(@"^[a-zA-Zא-ת\s]+$").WithMessage("שם פרטי יכול להכיל אותיות ורווחים בלבד");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("חובה להזין שם משפחה")
                .MinimumLength(2).WithMessage("שם משפחה חייב להכיל לפחות 2 תווים")
                .MaximumLength(50).WithMessage("שם משפחה לא יכול לעלות על 50 תווים")
                .Matches(@"^[a-zA-Zא-ת\s]+$").WithMessage("שם משפחה יכול להכיל אותיות ורווחים בלבד");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("חובה להזין כתובת אימייל")
                .EmailAddress().WithMessage("פורמט האימייל אינו תקין");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("חובה להזין סיסמה")
                .Length(8, 16).WithMessage("הסיסמה חייבת להיות בין 8 ל-16 תווים")
                .Matches(@"[A-Z]").WithMessage("הסיסמה חייבת לכלול לפחות אות גדולה אחת (A-Z)")
                .Matches(@"[a-z]").WithMessage("הסיסמה חייבת לכלול לפחות אות קטנה אחת (a-z)")
                .Matches(@"[0-9]").WithMessage("הסיסמה חייבת לכלול לפחות ספרה אחת")
                .Matches(@"[@$!%*?&]").WithMessage("הסיסמה חייבת לכלול לפחות תו מיוחד אחד (@$!%*?&)");
        }
    }
}
