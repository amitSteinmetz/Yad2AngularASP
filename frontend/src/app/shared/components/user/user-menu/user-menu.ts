import { Component, signal, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { UserMenuDropdown } from './user-menu-dropdown/user-menu-dropdown';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-user-menu',
  imports: [CommonModule, UserMenuDropdown],
  templateUrl: './user-menu.html',
  styleUrl: './user-menu.css',
})
export class UserMenu {
  router = inject(Router);
  authService = inject(AuthService);
  currentUser = this.authService.currentUser;
  isMenuOpen = signal(false);

  userInitials = computed(() => {
    const user = this.currentUser();
    if (!user || !user.firstName || !user.lastName) return '';
    return (user.firstName[0] + user.lastName[0]).toUpperCase();
  });

  toggleMenu() {
    if (!this.currentUser()) {
      this.router.navigate(['/auth/login']);
    }
  }

  closeMenu() {
    this.isMenuOpen.set(false);
  }
}
