import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { CustomResponse } from '@models/custom-response';
import { Favorito } from '@models/favorito.model';
import { Produto } from '@models/produto.model';
import { AutenticacaoService } from '@services/autenticacao.service';
import { BaseService } from '@services/base.service';
import { FavoritosService } from '@services/favoritos.service';
import { BehaviorSubject, Observable, tap } from 'rxjs';

type ProdutosGetAllParams = {
  busca?: string;
  categoriaId?: string;
  vendedorId?: string;
};

@Injectable({
  providedIn: 'root'
})
export class ProdutosService extends BaseService {
  _produtosSubject = new BehaviorSubject<Produto[]>([]);
  produtos$ = this._produtosSubject.asObservable();

  private _http = inject(HttpClient);
  private _favoritosService = inject(FavoritosService);
  private _autenticacaoService = inject(AutenticacaoService);

  protected usuarioLogado: boolean = false;

  _favoritosSubject = new BehaviorSubject<Favorito[]>([]);

  constructor() {
    super();

    this._autenticacaoService.usuarioLogado.subscribe(v => {
      this.usuarioLogado = v;

      if (v) {
        const produtos = this._produtosSubject.getValue();
        this.carregarFavoritos(produtos);
        this._produtosSubject.next([...produtos]);
      }
      else {
        const produtos = this._produtosSubject.getValue();

        produtos.forEach(p => {
          p.favoritoId = null;
        });

        this._produtosSubject.next([...produtos]);
      }
    });
  }

  obterTodos({ busca = '', categoriaId = '', vendedorId = '' }: ProdutosGetAllParams) {
    let httpParams = new HttpParams()
    if (busca) httpParams = httpParams.append('busca', busca)
    if (categoriaId) httpParams = httpParams.append('categoriaId', categoriaId);
    if (vendedorId) httpParams = httpParams.append('vendedorId', vendedorId);
    return this._http
      .get<CustomResponse<Produto[]>>(`${this.apiUrl}/Produtos?${httpParams.toString()}`)
      .pipe(
        tap({
          next: (response) => {
            if (response.success) {
              response.data.forEach((produto) => {
                produto.imagem = `${environment.imagesBaseUrl}/${produto.imagem}`;
              });

              this.carregarFavoritos(response.data);
              this._produtosSubject.next(response.data);
            }
          }
        })
      );
  }

  obterPorId(id: string) {
    return this._http
      .get<CustomResponse<Produto>>(`${this.apiUrl}/Produtos/${id}`)
      .pipe(
        tap((response) => {
          if (response.success) {
            response.data.imagem = `${environment.imagesBaseUrl}/${response.data.imagem}`;
            response.data.produtosVendedor.forEach((produto) => {
              produto.imagem = `${environment.imagesBaseUrl}/${produto.imagem}`;
            });
            this.carregarFavoritos([response.data]);
          }
        })
      );
  }

  alternarFavorito(produto: Produto) {
    return new Observable<Produto>((sub) => {
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

      sub.next(produto);
    })
  }

  private carregarFavoritos(produtos: Produto[]) {
    if (!this.usuarioLogado) {
      return;
    }

    this._favoritosService.buscarTodos().subscribe({
      next: (favoritos) => {
        produtos.forEach(p => {
          const favorito = favoritos.data.find(f => f.produto.id === p.id);

          if (!favorito) {
            return;
          }

          p.favoritoId = favorito.id;
        });
      }
    });
  }
}
