import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class AuthValidators {
  static passwordMatch(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password');
    const repeatPassword = control.get('repeatPassword');

    if (password && repeatPassword && password.value !== repeatPassword.value) {
      const errors = { passwordMismatch: true };
      repeatPassword.setErrors({ ...repeatPassword.errors, ...errors });
      return errors;
    }

    // Clear the error if it was set by this validator
    if (repeatPassword?.hasError('passwordMismatch')) {
      const remainingErrors = { ...repeatPassword.errors };
      delete remainingErrors['passwordMismatch'];
      repeatPassword.setErrors(Object.keys(remainingErrors).length ? remainingErrors : null);
    }

    return null;
  }
}
