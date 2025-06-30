import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { CustomResponse } from '../../../../models/custom-response';
import { Produto } from '../../../../models/produto';
import { BaseService } from '../../../../services/base.service';

@Injectable()
export class ProdutosService extends BaseService {
  private _http = inject(HttpClient);
  private _apiUrl = environment.apiUrl;

  obterTodos() {
    return this._http
      .get<CustomResponse<Produto[]>>(`${this._apiUrl}/Produtos`)
      .pipe(
        tap((response) => {
          if (response.success) {
            response.data.forEach((produto) => {
              produto.imagem = `${environment.imagesBaseUrl}/${produto.imagem}`;
            });
          }
        })
      );
  }

  obterPorId(id: string) {
    return this._http
      .get<CustomResponse<Produto>>(`${this._apiUrl}/Produtos/${id}`)
      .pipe(
        tap((response) => {
          if (response.success) {
            response.data.imagem = `${environment.imagesBaseUrl}/${response.data.imagem}`;
          }
        })
      );
  }

  adicionarFavoritos(produtoId: string): Observable<CustomResponse<any>> {
    const url = `${this._apiUrl}/Produtos/adicionar-favorito/${produtoId}`; // temporario
    return this._http.post<CustomResponse<any>>(url, {});
  }

  removerFavoritos(produtoId: string): Observable<CustomResponse<any>> {
    const url = `${this._apiUrl}/Produtos/remover-favorito/${produtoId}`; // temporario
    return this._http.delete<CustomResponse<any>>(url);
  }
}
