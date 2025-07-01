import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as regular from '@fortawesome/free-regular-svg-icons';
import * as solid from '@fortawesome/free-solid-svg-icons';
import { Produto } from '@models/produto.model';
import { FavoritosService } from '@services/favoritos.service';
import { ToasterService } from '@services/toaster.service';
import { ProdutosService } from '../services/produtos/produtos.service';

@Component({
  selector: 'app-lista-produto',
  standalone: false,
  templateUrl: './lista-produto.component.html',
  styleUrl: './lista-produto.component.css',
})
export class ListaProdutoComponent implements OnInit {
  faStarSolid = solid.faStar;
  faStarRegular = regular.faStar;

  private _produtoService = inject(ProdutosService);
  private _favoritoService = inject(FavoritosService);
  private _toasterService = inject(ToasterService);
  private _route = inject(ActivatedRoute);
  private _router = inject(Router);
  produtos: Produto[] = [];

  ngOnInit(): void {
    this._route.queryParams.subscribe((params) => {
      this._produtoService.getAll(this._router.url).subscribe({
        next: (res) => {
          this.produtos = res.data;
        },
        error: (err) => this._toasterService.error(err?.error?.errors),
      });
    });
  }

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
