import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { CustomResponse } from '../../../../models/custom-response';
import { BaseService } from '../../../../services/base.service';
import { Produto } from '../../../../models/produto';

@Injectable()
export class ProdutosService extends BaseService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  obterTodos() {
    return this.http.get<CustomResponse<Produto[]>>(`${this.apiUrl}/Produtos`);
  }

  obterPorId(id: string) {
    return this.http.get<CustomResponse<Produto>>(
      `${this.apiUrl}/Produtos/${id}`
    );
  }
}
