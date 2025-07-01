import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "@environments/environment";
import { CustomResponse } from "@models/custom-response";
import { Produto } from "@models/produto.model";
import { Observable, tap } from "rxjs";
import { BaseService } from "./base.service";

@Injectable()
export class FavoritosService extends BaseService {
    _http = inject(HttpClient);
    _favoritosUrl = `${this.apiUrl}/Favoritos`

    buscarPorClienteId(): Observable<CustomResponse<{ produto: Produto }[]>> {
        const url = `${this._favoritosUrl}`;
        return this._http.get<CustomResponse<{ produto: Produto }[]>>(url).pipe(
            tap(res => {
                if (res.success) {
                    res.data.map(data => {
                        data.produto.imagem = `${environment.imagesBaseUrl}/${data.produto.imagem}`
                    })
                }
            })
        )
    }

    adicionar(produtoId: string): Observable<CustomResponse<any>> {
        const url = `${this._favoritosUrl}/${produtoId}`;
        return this._http.post<CustomResponse<any>>(url, {});
    }

    delete(produtoId: string): Observable<CustomResponse<any>> {
        const url = `${this._favoritosUrl}/${produtoId}`;
        return this._http.delete<CustomResponse<any>>(url);
    }
}