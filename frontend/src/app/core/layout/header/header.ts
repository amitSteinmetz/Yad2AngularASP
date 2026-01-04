import { Component, inject, signal } from '@angular/core';
import { NavigationService } from '../../services/navigation.service';
import { RouterLink } from '@angular/router';
import { PublishButton } from '../../../shared/components/publish-button/publish-button';
import { UserMenu } from '../../../shared/components/user/user-menu/user-menu';
import { CategoryItemModal } from './category-item-modal/category-item-modal';
import { CategoryItem } from '../../constants/navigation.constants';

@Component({
  selector: 'app-header',
  imports: [RouterLink, PublishButton, UserMenu, CategoryItemModal],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  navigationService = inject(NavigationService);
  categories = this.navigationService.getCategories();
  isMobileMenuOpen = signal(false);
  activeCategory = signal<CategoryItem | null>(null);

  toggleMobileMenu() {
    this.isMobileMenuOpen.update((v) => !v);
  }

  closeMobileMenu() {
    this.isMobileMenuOpen.set(false);
  }

  onCategoryHover(category: CategoryItem | null) {
    this.activeCategory.set(category);
  }
}
