import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: '../login/login.css', // Reusing the same CSS
})
export class RegisterPage {
  firstName = '';
  lastName = '';
  email = '';
  password = '';

  onSubmit() {
    console.log('Register attempt:', {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      password: this.password,
    });
  }
}
