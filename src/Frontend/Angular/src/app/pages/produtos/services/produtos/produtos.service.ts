import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { CustomResponse } from '@models/custom-response';
import { Produto } from '@models/produto.model';
import { BaseService } from '@services/base.service';
import { tap } from 'rxjs';

@Injectable()
export class ProdutosService extends BaseService {
  private _http = inject(HttpClient);

  getAll(url: string) {
    return this._http
      .get<CustomResponse<Produto[]>>(`${this.apiUrl}${url}`)
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
