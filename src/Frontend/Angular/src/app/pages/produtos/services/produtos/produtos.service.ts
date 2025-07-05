import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { CustomResponse } from '@models/custom-response';
import { Produto } from '@models/produto.model';
import { BaseService } from '@services/base.service';
import { BehaviorSubject, tap } from 'rxjs';

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
          }
        })
      );
  }
}
