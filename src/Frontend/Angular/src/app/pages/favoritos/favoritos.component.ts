import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { map, Observable } from 'rxjs';
import { Produto } from '../../models/produto';
import { ToasterService } from '../../services/toaster.service';
import { ProdutosService } from '../produtos/services/produtos/produtos.service';

@Component({
  selector: 'app-favoritos',
  imports: [AsyncPipe, FontAwesomeModule, RouterLink, CurrencyPipe],
  templateUrl: './favoritos.component.html',
  providers: [ProdutosService],
})
export class FavoritosComponent {
  _toasterService = inject(ToasterService);
  _produtosService = inject(ProdutosService);
  favoritos$: Observable<Produto[]> = this._produtosService
    .obterTodos()
    .pipe(map((res) => res.data));

  faTrashCan = faTrashCan;

  removerFavorito(produtoId: string) {
    this._produtosService.removerFavoritos(produtoId).subscribe({
      next: () => {
        this._toasterService.success('Produto removido de favoritos!');
      },
      error: () => {
        this._toasterService.error();
      },
    });
  }
}
