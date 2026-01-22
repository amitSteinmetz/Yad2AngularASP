import { Component, input, output, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../../core/services/auth.service';

@Component({
  selector: 'app-user-menu-dropdown',
  imports: [],
  templateUrl: './user-menu-dropdown.html',
  styleUrl: './user-menu-dropdown.css',
})
export class UserMenuDropdown {
  isOpen = input.required<boolean>();
  onMouseEnter = output<void>();

  router = inject(Router);
  authService = inject(AuthService);

  handleMouseEnter() {
    this.onMouseEnter.emit();
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error('Logout failed:', err);
      },
    });
  }
}
