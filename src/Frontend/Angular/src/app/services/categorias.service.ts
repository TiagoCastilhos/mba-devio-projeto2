import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Categoria } from '../models/categoria';
import { CustomResponse } from '../models/custom-response';

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;
  
  constructor() { }

  getAll() {
    return this.http.get<CustomResponse<Categoria[]>>(
      `${this.apiUrl}/Categorias`
    );
  }
}
