import { Component, inject } from '@angular/core';
import * as regular from '@fortawesome/free-regular-svg-icons';
import * as solid from '@fortawesome/free-solid-svg-icons';
import { map, Observable } from 'rxjs';
import { Produto } from '../../../models/produto';
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
  _toasterService = inject(ToasterService);
  produtos$: Observable<Produto[]> = this._produtoService
    .obterTodos()
    .pipe(map((res) => res.data));

  alternarFavorito(produto: Produto) {
    //verificar se o usuario esta logado, se nao estiver mandar ele pra tela de login
    //chamar a api quando pronta
    produto.favorito = !(produto.favorito ?? false);

    !produto.favorito
      ? this._produtoService.adicionarFavoritos(produto.id).subscribe({
        next: (res) => this._toasterService.success(),
        error: (err) => this._toasterService.error(),
      })
      : this._produtoService.removerFavoritos(produto.id).subscribe({
        next: (res) => this._toasterService.success(),
        error: (err) => this._toasterService.error(),
      });
  }
}
