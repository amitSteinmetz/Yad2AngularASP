import { Component, inject } from '@angular/core';
import { NavigationService } from '../../core/services/navigation.service';
import { RouterLink } from '@angular/router';
import { BANNERS } from '../../core/constants/home-page.constants';
import { ApartmentCard } from '../../shared/components/apartment-card/apartment-card';
import { Carousel } from '../../shared/components/carousel/carousel';

@Component({
  selector: 'app-home-page',
  imports: [RouterLink, ApartmentCard, Carousel],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage {
  navigationService = inject(NavigationService);
  categories = this.navigationService.getCategories();
  popularApartment = this.navigationService.getPopularApartment();
  banners = BANNERS;
}
