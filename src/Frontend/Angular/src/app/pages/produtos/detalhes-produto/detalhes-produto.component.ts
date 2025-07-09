import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Produto } from '@models/produto.model';
import { ToasterService } from '@services/toaster.service';
import { BehaviorSubject } from 'rxjs';
import { ProdutosService } from '../services/produtos/produtos.service';

@Component({
  selector: 'app-detalhes-produto',
  standalone: false,
  templateUrl: './detalhes-produto.component.html',
})
export class DetalhesProdutoComponent {
  private _produtoService = inject(ProdutosService);
  private _toasterService = inject(ToasterService);
  private _route = inject(ActivatedRoute);
  private _produto = new BehaviorSubject<Produto | undefined>(undefined);
  private _produtosVendedor = new BehaviorSubject<Produto[]>([]);

  favoritoId: string | null = null;
  produto$ = this._produto.asObservable();
  produtosVendedor$ = this._produtosVendedor.asObservable();

  ngOnInit() {
    this._route.paramMap.subscribe(p => {
      const produtoId = p.get('id')!;

      this._produtoService.obterPorId(produtoId)
        .subscribe({
          next: (res) => {
            if (res.success) {
              this._produto.next(res.data);
              this._produtoService
                .obterTodos({ vendedorId: res.data.vendedorId })
                .subscribe({
                  next: (res) => {
                    this._produtosVendedor.next(res.data);
                  },
                });
            }
          },
          error: (err) => {
            this._toasterService.erro(err.error.errors);
          },
        });
    });

  }

  alternarFavorito(produto: Produto) {
    this._produtoService.alternarFavorito(produto).subscribe({
      next: (res) => {
        const produtoSelecionado = this._produto.getValue()!

        if (produtoSelecionado.id === res.id) {
          this._produto.next(res);
        }

        const produtosVendedor = this._produtosVendedor.getValue()!;
        const produtoVendedorIndex = produtosVendedor.findIndex(pv => pv.id === res.id);

        if (produtoVendedorIndex !== -1) {
          produtosVendedor[produtoVendedorIndex] = res;
          this._produtosVendedor.next([...produtosVendedor]);
        }
      }
    });
  }

  obterProdutosDoVendedor(vendedorId: string) {
    this._produtoService
      .obterTodos({ vendedorId })
      .subscribe(() => this._toasterService.sucesso());
  }
}
