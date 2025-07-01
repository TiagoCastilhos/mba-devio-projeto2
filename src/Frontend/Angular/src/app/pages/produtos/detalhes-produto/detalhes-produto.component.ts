import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, Observable, tap } from 'rxjs';
import { CustomResponse } from '../../../models/custom-response';
import { Produto } from '../../../models/produto.model';
import { FavoritosService } from '../../../services/favoritos.service';
import { ToasterService } from '../../../services/toaster.service';
import { ProdutosService } from '../services/produtos/produtos.service';

@Component({
  selector: 'app-detalhes-produto',
  standalone: false,
  templateUrl: './detalhes-produto.component.html',
})
export class DetalhesProdutoComponent {
  _produtoService = inject(ProdutosService);
  _favoritoService = inject(FavoritosService);
  _toasterService = inject(ToasterService);
  _route = inject(ActivatedRoute);
  produto$: Observable<Produto> = this._produtoService
    .obterPorId(this._route.snapshot.paramMap.get('id')!)
    .pipe(
      map((res: CustomResponse<Produto>) => res.data),
      tap({
        next: (res: Produto) => this.favorito = res.favorito ?? false,
        error: (error) => this._toasterService.error(error?.error?.errors)
      }));
  favorito = false;

  adicionarFavorito(produtoId: string) {
    this._favoritoService.adicionar(produtoId).subscribe({
      next: (res) => {
        if (!res.success) throw Error();
        this._toasterService.success();
        this.favorito = true;
      },
      error: (error) => {
        this._toasterService.error(error?.error?.errors);
      }
    });
  }
}
