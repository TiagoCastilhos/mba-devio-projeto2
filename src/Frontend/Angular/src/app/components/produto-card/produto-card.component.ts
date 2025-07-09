import { CurrencyPipe } from '@angular/common';
import { Component, inject, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import * as regular from '@fortawesome/free-regular-svg-icons';
import * as solid from '@fortawesome/free-solid-svg-icons';
import { Produto } from '@models/produto.model';
import { ProdutosService } from '../../pages/produtos/services/produtos/produtos.service';

@Component({
  selector: 'app-produto-card',
  imports: [RouterLink, FontAwesomeModule, CurrencyPipe],
  templateUrl: './produto-card.component.html'
})
export class ProdutoCardComponent {
  produto = input.required<Produto>();
  faStarSolid = solid.faStar;
  faStarRegular = regular.faStar;

  private _produtosService = inject(ProdutosService);

  alternarFavorito(produto: Produto) {
    this._produtosService.alternarFavorito(produto)
      .subscribe(p => produto = p);
  };
}
