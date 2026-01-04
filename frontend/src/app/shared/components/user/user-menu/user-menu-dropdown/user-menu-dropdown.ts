import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-user-menu-dropdown',
  imports: [],
  templateUrl: './user-menu-dropdown.html',
  styleUrl: './user-menu-dropdown.css',
})
export class UserMenuDropdown {
  isOpen = input.required<boolean>();
  onMouseEnter = output<void>();

  handleMouseEnter() {
    this.onMouseEnter.emit();
  }
}
