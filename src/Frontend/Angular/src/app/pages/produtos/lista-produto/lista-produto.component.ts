import { Component, inject } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ToasterService } from '../../../services/toaster.service';
import { Produto } from '../models/produto';
import { ProdutosService } from '../services/produtos/produtos.service';

@Component({
  selector: 'app-lista-produto',
  standalone: false,
  templateUrl: './lista-produto.component.html',
})
export class ListaProdutoComponent {
  _produtoService = inject(ProdutosService);
  _toasterService = inject(ToasterService);
  produtos$: Observable<Produto[]> = this._produtoService
    .obterTodos()
    .pipe(map((res) => res.data));
}
