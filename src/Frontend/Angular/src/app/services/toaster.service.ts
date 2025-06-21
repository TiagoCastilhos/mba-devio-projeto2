import { inject, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  private toastrService = inject(ToastrService);

  success(message: string = '', title: string = 'Sucesso!') {
    this.toastrService.success(message, title);
  }

  warning(message: string = '', title: string = 'Atenção!') {
    this.toastrService.warning(message, title);
  }

  error(message: string = 'Ocorreu um erro!', title: string = 'Opa :(') {
    this.toastrService.error(message, title);
  }
}
