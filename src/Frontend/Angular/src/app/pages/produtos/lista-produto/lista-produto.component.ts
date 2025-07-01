import { Component, inject } from '@angular/core';
import * as regular from '@fortawesome/free-regular-svg-icons';
import * as solid from '@fortawesome/free-solid-svg-icons';
import { map, Observable, tap } from 'rxjs';
import { Produto } from '../../../models/produto.model';
import { FavoritosService } from '../../../services/favoritos.service';
import { ToasterService } from '../../../services/toaster.service';
import { ProdutosService } from '../services/produtos/produtos.service';

@Component({
  selector: 'app-lista-produto',
  standalone: false,
  templateUrl: './lista-produto.component.html',
  styleUrl: './lista-produto.component.css',
})
export class ListaProdutoComponent {
  faStarSolid = solid.faStar;
  faStarRegular = regular.faStar;

  _produtoService = inject(ProdutosService);
  _favoritoService = inject(FavoritosService);
  _toasterService = inject(ToasterService);
  produtos$: Observable<Produto[]> = this._produtoService.obterTodos().pipe(
    tap({ error: (err) => this._toasterService.error(err?.error?.errors) }),
    map((res) => res.data)
  );

  alternarFavorito(produto: Produto) {
    !produto.favorito
      ? this._favoritoService.adicionar(produto.id).subscribe({
          next: (res) => {
            if (res.success) {
              this._toasterService.success();
              //verificar se o usuario esta logado, se nao estiver mandar ele pra tela de login
              //chamar a api quando pronta
              produto.favorito = !(produto.favorito ?? false);
            }
          },
          error: (err) => this._toasterService.error(),
        })
      : this._favoritoService.delete(produto.id).subscribe({
          next: (res) => {
            if (res.success) {
              this._toasterService.success();
              produto.favorito = !(produto.favorito ?? false);
            }
          },
          error: (err) => this._toasterService.error(err?.error?.errors),
        });
  }
}
