import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { map, Observable } from 'rxjs';
import { Produto } from '../../models/produto.model';
import { FavoritosService } from '../../services/favoritos.service';
import { ToasterService } from '../../services/toaster.service';
import { ProdutosService } from '../produtos/services/produtos/produtos.service';

@Component({
  selector: 'app-favoritos',
  imports: [AsyncPipe, FontAwesomeModule, RouterLink, CurrencyPipe],
  templateUrl: './favoritos.component.html',
  providers: [ProdutosService, FavoritosService],
})
export class FavoritosComponent {
  _toasterService = inject(ToasterService);
  _favoritosService = inject(FavoritosService);
  favoritos$: Observable<{ produto: Produto }[]> = this._favoritosService
    .buscarPorClienteId()
    .pipe(map((res) => res.data));

  faTrashCan = faTrashCan;

  removerFavorito(produtoId: string) {
    this._favoritosService.delete(produtoId).subscribe({
      next: () => {
        this._toasterService.success('Produto removido de favoritos!');
      },
      error: () => {
        this._toasterService.error();
      },
    });
  }
}
