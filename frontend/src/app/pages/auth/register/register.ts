import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: '../login/login.css', // Reusing the same CSS
})
export class RegisterPage {
  fullName = '';
  email = '';
  password = '';
  authService = inject(AuthService);

  onSubmit() {
    console.log('Register attempt:', {
      fullName: this.fullName,
      email: this.email,
      password: this.password,
    });

    this.authService.register({
      fullName: this.fullName,
      email: this.email,
      password: this.password,
    });
  }
}
