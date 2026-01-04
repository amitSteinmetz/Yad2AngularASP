import { Component, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { User } from '../../../../core/models/user.model';
import { UserMenuDropdown } from './user-menu-dropdown/user-menu-dropdown';

@Component({
  selector: 'app-user-menu',
  imports: [CommonModule, UserMenuDropdown],
  templateUrl: './user-menu.html',
  styleUrl: './user-menu.css',
})
export class UserMenu {
  // Mocking the user state for now as requested
  user = signal<User | null>({
    firstName: 'עמית',
    lastName: 'שטיינמץ',
    email: 'amit@example.com',
    isLoggedIn: true,
    // avatarUrl: 'https://lh3.googleusercontent.com/a/ACg8ocL...', // Uncomment to test image
  });

  isMenuOpen = signal(false);

  userInitials = computed(() => {
    const u = this.user();
    if (!u) return '';
    return (u.firstName[0] + u.lastName[0]).toUpperCase();
  });

  toggleMenu() {
    if (this.user()) {
      this.isMenuOpen.update((v) => !v);
    }
  }

  closeMenu() {
    this.isMenuOpen.set(false);
  }
}
