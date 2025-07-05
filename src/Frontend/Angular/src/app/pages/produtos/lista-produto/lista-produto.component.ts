import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import * as regular from '@fortawesome/free-regular-svg-icons';
import * as solid from '@fortawesome/free-solid-svg-icons';
import { Produto } from '@models/produto.model';
import { FavoritosService } from '@services/favoritos.service';
import { Subscription } from 'rxjs';
import { ProdutosService } from '../services/produtos/produtos.service';
import { AuthenticationService } from '@services/authentication.service';

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
  private _favoritosService = inject(FavoritosService);
  private _authenticationService = inject(AuthenticationService);
  produtos: Produto[] = [];
  private subscription!: Subscription;

  ngOnInit(): void {
    this.subscription = this._produtoService.produtos$.subscribe({
      next: (res) => {
        this.produtos = res;

        if (!this._authenticationService.estaLogado()) {
          return;
        }

        this._favoritosService.buscarTodos().subscribe({
          next: (favoritos) => {
            if (!favoritos.success || !this.produtos) {
              return;
            }

            this.produtos.forEach(p => {
              const favorito = favoritos.data.find(f => f.produto.id === p.id);
              if (!favorito) {
                return;
              }

              p.favoritoId = favorito.id;
            });
          }
        });
      },
    });
  }

  alternarFavorito(produto: Produto) {
    !produto.favoritoId
      ? this._favoritosService.adicionar(produto.id).subscribe({
        next: (res) => {
          produto.favoritoId = res.id;
        }
      })
      : this._favoritosService.deletar(produto.favoritoId).subscribe({
        next: () => {
          produto.favoritoId = null;
        }
      });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
