import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomResponse } from '@models/custom-response';
import { Produto } from '@models/produto.model';
import { FavoritosService } from '@services/favoritos.service';
import { ToasterService } from '@services/toaster.service';
import { map, Observable, tap } from 'rxjs';
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
        next: (response: Produto) => {
          this._produtoService
            .obterTodos({ vendedorId: response.vendedorId })
            .subscribe({
              next: (res) => {
                this.produtosVendedor = res.data;
              },
            });
        },
        error: (error) => this._toasterService.erro(error?.error?.errors),
      })
    );
  favoritoId: string | null = null;
  produtosVendedor: Produto[] = [];

  adicionarFavorito(produto: Produto) {
    this._favoritoService.adicionar(produto.id).subscribe({
      next: (res) => {
        if (res.success) {
          this._toasterService.sucesso(produto.nome + ' favoritado!');
          this.favoritoId = res.data.id;
        }
      },
      error: (response) => {
        this._toasterService.erro(response.error.errors.toString());
      },
    });
  }

  produtosGetAll(vendedorId: string) {
    this._produtoService
      .obterTodos({ vendedorId })
      .subscribe(() => this._toasterService.sucesso());
  }
}
