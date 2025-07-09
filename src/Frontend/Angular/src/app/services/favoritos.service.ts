import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { CustomResponse } from '@models/custom-response';
import { Favorito } from '@models/favorito.model';
import { Observable, tap } from 'rxjs';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class FavoritosService extends BaseService {
  private _http = inject(HttpClient);
  private _favoritosUrl = `${this.apiUrl}/Favoritos`;

  buscarTodos(): Observable<CustomResponse<Favorito[]>> {
    return this._http.get<CustomResponse<Favorito[]>>(this._favoritosUrl)
      .pipe(
        tap({
          next: (res) => {
            if (res.success) {
              res.data.map((data) => {
                data.produto.imagem = `${environment.imagesBaseUrl}/${data.produto.imagem}`;
              });
            }
          }
        })
      );
  }

  adicionar(produtoId: string): Observable<any> {
    return this._http.post<any>(`${this._favoritosUrl}/${produtoId}`, {});
  }

  deletar(favoritoId: string): Observable<any> {
    return this._http.delete<any>(`${this._favoritosUrl}/${favoritoId}`);
  }
}
