import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import * as regular from '@fortawesome/free-regular-svg-icons';
import * as solid from '@fortawesome/free-solid-svg-icons';
import { Produto } from '@models/produto.model';
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
    this._produtoService.alternarFavorito(produto);
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
