export const AUTH_PATTERNS = {
  PASSWORD: {
    UPPERCASE: /[A-Z]/,
    LOWERCASE: /[a-z]/,
    NUMBER: /[0-9]/,
    SPECIAL: /[@$!%*?&]/,
  },
  NAME: /^[a-zA-Zא-ת\s]+$/,
};

export const AUTH_VALIDATION = {
  PASSWORD: {
    MIN_LENGTH: 8,
    MAX_LENGTH: 16,
  },
  NAME: {
    MIN_LENGTH: 2,
    MAX_LENGTH: 50,
  },
};

export const AUTH_MESSAGES = {
  EMAIL: {
    REQUIRED: 'מייל הוא שדה חובה',
    INVALID: 'פורמט האימייל אינו תקין',
  },
  PASSWORD: {
    REQUIRED: 'סיסמה היא שדה חובה',
    LENGTH: `הסיסמה חייבת להיות בין ${AUTH_VALIDATION.PASSWORD.MIN_LENGTH} ל-${AUTH_VALIDATION.PASSWORD.MAX_LENGTH} תווים`,
    UPPERCASE: 'הסיסמה חייבת לכלול לפחות אות גדולה אחת',
    LOWERCASE: 'הסיסמה חייבת לכלול לפחות אות קטנה אחת',
    NUMBER: 'הסיסמה חייבת לכלול לפחות מספר אחד',
    SPECIAL: 'הסיסמה חייבת לכלול לפחות תו מיוחד אחד',
    MISMATCH: 'הסיסמאות אינן תואמות',
  },
  FIRST_NAME: {
    REQUIRED: 'חובה להזין שם פרטי',
    LENGTH: `שם פרטי חייב להכיל לפחות ${AUTH_VALIDATION.NAME.MIN_LENGTH} תווים`,
    MAX_LENGTH: `שם פרטי לא יכול לעלות על ${AUTH_VALIDATION.NAME.MAX_LENGTH} תווים`,
    PATTERN: 'שם פרטי יכול להכיל אותיות ורווחים בלבד',
  },
  LAST_NAME: {
    REQUIRED: 'חובה להזין שם משפחה',
    LENGTH: `שם משפחה חייב להכיל לפחות ${AUTH_VALIDATION.NAME.MIN_LENGTH} תווים`,
    MAX_LENGTH: `שם משפחה לא יכול לעלות על ${AUTH_VALIDATION.NAME.MAX_LENGTH} תווים`,
    PATTERN: 'שם משפחה יכול להכיל אותיות ורווחים בלבד',
  },
};
