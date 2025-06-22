import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  private router = inject(Router);
  private authenticationService = inject(AuthenticationService);

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return this.authenticationService.isLoggedIn();
  }
}
