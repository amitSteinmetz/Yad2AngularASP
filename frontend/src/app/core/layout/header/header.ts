import { Component, inject } from '@angular/core';
import { NavigationService } from '../../services/navigation.service';
import { RouterLink } from '@angular/router';
import { PublishButton } from '../../../shared/components/publish-button/publish-button';

@Component({
  selector: 'app-header',
  imports: [RouterLink, PublishButton],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  navigationService = inject(NavigationService);
  categories = this.navigationService.getCategories();
}
