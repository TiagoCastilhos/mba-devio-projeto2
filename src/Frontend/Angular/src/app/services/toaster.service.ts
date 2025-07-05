import { inject, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  private toastrService = inject(ToastrService);

  sucesso(message: string = '', title: string = 'Sucesso!') {
    this.toastrService.success(message, title);
  }

  aviso(message: string = '', title: string = 'Atenção!') {
    this.toastrService.warning(message, title);
  }

  erro(message: string = 'Ocorreu um erro!', title: string = 'Opa :(') {
    this.toastrService.error(message, title);
  }
}
