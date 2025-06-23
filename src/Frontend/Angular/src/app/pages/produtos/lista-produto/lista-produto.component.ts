import { Component, inject, OnInit } from '@angular/core';
import { ProdutosService } from '../services/produtos/produtos.service';
import { Produto } from '../models/produto';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-lista-produto',
  standalone: false,
  templateUrl: './lista-produto.component.html',
})
export class ListaProdutoComponent {
  _produtoService = inject(ProdutosService);
  produtos$: Observable<Produto[]> = this._produtoService.obterTodos();
}
