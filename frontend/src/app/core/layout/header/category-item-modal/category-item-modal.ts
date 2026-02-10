import { Component, input } from '@angular/core';
import { CategoryItem } from '../../../constants/navigation.constants';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-category-item-modal',
  imports: [RouterLink],
  templateUrl: './category-item-modal.html',
  styleUrl: './category-item-modal.css',
})
export class CategoryItemModal {
  categoryItem = input.required<CategoryItem>();
}
