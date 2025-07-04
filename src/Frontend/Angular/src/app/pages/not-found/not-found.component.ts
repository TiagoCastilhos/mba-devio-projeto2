import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faFaceSadTear } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-not-found',
  imports: [RouterLink, FontAwesomeModule],
  templateUrl: './not-found.component.html',
})
export class NotFoundComponent {
  faFaceSadTear = faFaceSadTear;
}
