import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';
import { CustomResponse } from '@models/custom-response';
import { Favorito } from '@models/favorito.model';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { BaseService } from './base.service';
import { ToasterService } from './toaster.service';

@Injectable()
export class FavoritosService extends BaseService {
  private _http = inject(HttpClient);
  private _toasterService = inject(ToasterService);
  private _router = inject(Router);
  private _favoritosUrl = `${this.apiUrl}/Favoritos`;

  buscarPorClienteId(): Observable<CustomResponse<Favorito[]>> {
    const url = `${this._favoritosUrl}`;
    return this._http.get<CustomResponse<Favorito[]>>(url).pipe(
      tap({
        next: (res) => {
          if (res.success) {
            res.data.map((data) => {
              data.produto.imagem = `${environment.imagesBaseUrl}/${data.produto.imagem}`;
            });
          }
        },
      }),
      catchError((response) => {
        if (response.status === 401) {
          this.isUnauthenticated();
        }
        return throwError(() => response);
      }),
      catchError(this.serviceError)
    );
  }

  adicionar(produtoId: string): Observable<CustomResponse<any>> {
    const url = `${this._favoritosUrl}/${produtoId}`;
    return this._http.post<CustomResponse<any>>(url, {}).pipe(
      catchError((response) => {
        if (response.status === 401) {
          this.isUnauthenticated();
        }
        return throwError(() => response);
      }),
      catchError(this.serviceError)
    );
  }

  delete(favoritoId: string): Observable<CustomResponse<any>> {
    const url = `${this._favoritosUrl}/${favoritoId}`;
    return this._http.delete<CustomResponse<any>>(url).pipe(
      catchError((response) => {
        if (response.status === 401) {
          this.isUnauthenticated();
        }
        return throwError(() => response);
      }),
      catchError(this.serviceError)
    );
  }

  isUnauthenticated() {
    this._toasterService.error(
      'Favor fazer o login ou registrar.',
      'Usuário não autenticado!'
    );
    this._router.navigate(['/auth/login']);
  }
}
