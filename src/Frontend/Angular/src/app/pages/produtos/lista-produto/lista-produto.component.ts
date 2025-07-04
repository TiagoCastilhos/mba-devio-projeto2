import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import * as regular from '@fortawesome/free-regular-svg-icons';
import * as solid from '@fortawesome/free-solid-svg-icons';
import { Produto } from '@models/produto.model';
import { FavoritosService } from '@services/favoritos.service';
import { ToasterService } from '@services/toaster.service';
import { Subscription } from 'rxjs';
import { ProdutosService } from '../services/produtos/produtos.service';

@Component({
  selector: 'app-lista-produto',
  standalone: false,
  templateUrl: './lista-produto.component.html',
  styleUrl: './lista-produto.component.css',
})
export class ListaProdutoComponent implements OnInit, OnDestroy {
  faStarSolid = solid.faStar;
  faStarRegular = regular.faStar;

  private _produtoService = inject(ProdutosService);
  private _favoritoService = inject(FavoritosService);
  private _toasterService = inject(ToasterService);
  produtos: Produto[] = [];
  private subscription!: Subscription;

  ngOnInit(): void {
    this.subscription = this._produtoService.produtos$.subscribe({
      next: (res) => {
        this.produtos = res;
      },
    });
  }

  alternarFavorito(produto: Produto) {
    !produto.favoritoId
      ? this._favoritoService.adicionar(produto.id).subscribe({
          next: (res) => {
            if (res.success) {
              this._toasterService.success(produto.nome + ' favoritado!');
              produto.favoritoId = res.data.id;
            }
          },
          error: (response) => {
            this._toasterService.error(response.error.errors.toString());
          }
        })
      : this._favoritoService.delete(produto.favoritoId).subscribe({
          next: (res) => {
            this._toasterService.success(produto.nome + ' desfavoritado!');
            produto.favoritoId = null;
          },
          error: (response) => {
            this._toasterService.error(response.error.errors.toString());
          }
        });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
