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

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, FontAwesomeModule, CommonModule, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  faMagnifyingGlass = faMagnifyingGlass;

  private _router = inject(Router);
  private _authenticationService = inject(AuthenticationService);
  private _categoriasService = inject(CategoriasService);
  private _produtosService = inject(ProdutosService);
  protected busca = '';

  ngOnInit() {
    this.buscar({});
  }

  categorias$ = this._categoriasService.getAll().pipe(map((res) => res.data));

  obterNomeUsuario() {
    return this._authenticationService.obterUsuario()?.nome || '';
  }

  deslogar() {
    this._authenticationService.deslogar();
    this._router.navigate(['/']);
  }

  estaLogado(): boolean {
    return this._authenticationService.estaLogado();
  }

  buscar({ busca = '', categoriaId = '' }) {
    this._produtosService
      .obterTodos({ categoriaId, busca })
      .subscribe();
  }
}
