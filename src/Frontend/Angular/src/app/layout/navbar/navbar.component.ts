import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from '@services/authentication.service';
import { CategoriasService } from '@services/categorias.service';
import { map } from 'rxjs';
import { ProdutosService } from '../../pages/produtos/services/produtos/produtos.service';
import { ToasterService } from '@services/toaster.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, FontAwesomeModule, CommonModule, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  providers: [
  ]
})
export class NavbarComponent {
  faMagnifyingGlass = faMagnifyingGlass;

  private _router = inject(Router);
  private _authenticationService = inject(AuthenticationService);
  private _categoriasService = inject(CategoriasService);
  private _produtosService = inject(ProdutosService);
  private _toasterService = inject(ToasterService);
  protected busca = '';

  ngOnInit() {
    this.search({});
  }

  categorias$ = this._categoriasService.getAll()
    .pipe(map((res) => res.data));

  getUserName() {
    return this._authenticationService.getUser()?.uniqueName || '';
  }

  logout() {
    this._authenticationService.logout();
    this._router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return this._authenticationService.isLoggedIn();
  }

  search({ busca = '', categoriaId = '' }) {
    this._produtosService.getAll({ categoriaId, busca }).subscribe();
  }
}
