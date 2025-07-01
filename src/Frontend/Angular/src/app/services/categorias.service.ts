import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Categoria } from '@models/categoria.model';
import { CustomResponse } from '@models/custom-response';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriasService extends BaseService {
  private http = inject(HttpClient);

  getAll() {
    return this.http.get<CustomResponse<Categoria[]>>(
      `${this.apiUrl}/Categorias`
    );
  }
}
