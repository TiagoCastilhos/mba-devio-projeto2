import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from '@services/authentication.service';
import { CategoriasService } from '@services/categorias.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, FontAwesomeModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  faMagnifyingGlass = faMagnifyingGlass;

  private router = inject(Router);
  private authenticationService = inject(AuthenticationService);
  private categoriasService = inject(CategoriasService);

  categorias$ = this.categoriasService.getAll()
    .pipe(map((res) => res.data));

  getUserName() {
    return this.authenticationService.getUser()?.uniqueName || '';
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return this.authenticationService.isLoggedIn();
  }
}
