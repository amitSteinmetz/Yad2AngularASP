import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class LoginPage {
  email = '';
  password = '';
  authService = inject(AuthService);
  router = inject(Router);

  onSubmit() {
    console.log('Login attempt:', { email: this.email, password: this.password });
    this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.log('error: ', error.message);
        // console.error(error);
      },
    });
  }
}
