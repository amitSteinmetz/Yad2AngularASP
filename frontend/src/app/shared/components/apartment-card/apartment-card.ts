import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PopularApartment } from '../../../core/config/navigation.config';

@Component({
  selector: 'app-apartment-card',
  imports: [RouterLink],
  templateUrl: './apartment-card.html',
  styleUrl: './apartment-card.css',
})
export class ApartmentCard {
  apartment = input.required<PopularApartment>();
}
