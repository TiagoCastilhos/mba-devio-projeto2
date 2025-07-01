import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { ProdutosService } from '../services/produtos/produtos.service';

@Component({
  selector: 'app-detalhes-produto',
  standalone: false,
  templateUrl: './detalhes-produto.component.html',
})
export class DetalhesProdutoComponent {
  _produtoService = inject(ProdutosService);
  _route = inject(ActivatedRoute);
  produto$ = this._produtoService
    .obterPorId(this._route.snapshot.paramMap.get('id')!)
    .pipe(map((res) => res.data));

  adicionarFavorito(produtoId: string) {
    this._produtoService.adicionarFavoritos(produtoId);
  }
}
