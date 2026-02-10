import { Injectable } from '@angular/core';
import { POPULAR_APARTMENTS } from '../constants/home-page.constants';
import { CATEGORIES } from '../constants/navigation.constants';
@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  getCategories() {
    return CATEGORIES;
  }
  getPopularApartment() {
    return POPULAR_APARTMENTS;
  }
}
