import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { NotificationService } from '../../../core/services/notification.service';
import { AuthLayoutComponent } from '../../../shared/layouts/auth-layout/auth-layout';
import {
  AUTH_MESSAGES,
  AUTH_PATTERNS,
  AUTH_VALIDATION,
} from '../../../core/constants/auth.constants';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule, AuthLayoutComponent],
  templateUrl: './login.html',
  styleUrls: ['./login.css', '../auth-form.css'],
})
export class LoginPage {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private notificationService = inject(NotificationService);

  readonly authMessages = AUTH_MESSAGES;

  loginForm: FormGroup = this.fb.group({
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
  });

  showPassword = signal(false);

  togglePassword() {
    this.showPassword.update((val) => !val);
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: () => {
          this.router.navigate(['/']);
        },
        error: (error) => {
          this.notificationService.error(
            error.message || 'ההתחברות נכשלה. אנא בדוק את פרטי המשתמש ונסה שוב.',
          );
        },
      });
    }
  }
}
