import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { NotificationService } from '../../../core/services/notification.service';
import { StepIndicatorComponent } from '../../../shared/components/step-indicator/step-indicator.component';
import { AuthLayoutComponent } from '../../../shared/layouts/auth-layout/auth-layout';
import {
  AUTH_MESSAGES,
  AUTH_PATTERNS,
  AUTH_VALIDATION,
} from '../../../core/constants/auth.constants';
import { AuthValidators } from '../../../core/validators/auth.validators';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    StepIndicatorComponent,
    AuthLayoutComponent,
  ],
  templateUrl: './register.html',
  styleUrls: ['../auth-form.css'],
})
export class RegisterPage {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private notificationService = inject(NotificationService);

  readonly authMessages = AUTH_MESSAGES;

  registerForm: FormGroup = this.fb.group(
    {
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(AUTH_VALIDATION.NAME.MIN_LENGTH),
          Validators.maxLength(AUTH_VALIDATION.NAME.MAX_LENGTH),
          Validators.pattern(AUTH_PATTERNS.NAME),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(AUTH_VALIDATION.NAME.MIN_LENGTH),
          Validators.maxLength(AUTH_VALIDATION.NAME.MAX_LENGTH),
          Validators.pattern(AUTH_PATTERNS.NAME),
        ],
      ],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(AUTH_VALIDATION.PASSWORD.MIN_LENGTH),
          Validators.maxLength(AUTH_VALIDATION.PASSWORD.MAX_LENGTH),
          Validators.pattern(AUTH_PATTERNS.PASSWORD.UPPERCASE),
          Validators.pattern(AUTH_PATTERNS.PASSWORD.LOWERCASE),
          Validators.pattern(AUTH_PATTERNS.PASSWORD.NUMBER),
          Validators.pattern(AUTH_PATTERNS.PASSWORD.SPECIAL),
        ],
      ],
      repeatPassword: ['', [Validators.required]],
    },
    { validators: AuthValidators.passwordMatch },
  );

  showPassword = signal(false);
  showRepeatPassword = signal(false);
  currentStep = signal(1);

  togglePassword() {
    this.showPassword.update((v) => !v);
  }

  toggleRepeatPassword() {
    this.showRepeatPassword.update((v) => !v);
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe({
        next: () => {
          this.notificationService.success('נרשמת בהצלחה! כעת ניתן להתחבר');
          this.router.navigate(['/auth/login']);
        },
        error: (error) => {
          console.log('caught error', error);
          this.notificationService.error(
            error.message || 'אירעה שגיאה בתהליך ההרשמה. אנא נסה שוב.',
          );
        },
      });
    }
  }
}
