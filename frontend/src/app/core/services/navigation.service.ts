import { Injectable } from '@angular/core';
import { CATEGORIES, POPULAR_APARTMENTS } from '../config/navigation.config';
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
