import { HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { throwError } from 'rxjs';

export abstract class BaseService {
  protected apiUrl = environment.apiUrl;

  protected serviceError(response: Response | any) {
    let customError: string[] = [];
    let customResponse: { error: { errors: string[] } } = {
      error: { errors: [] },
    };

    if (response instanceof HttpErrorResponse) {
      if (response.statusText === 'Unknown Error' && !response.error.errors) {
        customError.push('Ocorreu um erro desconhecido');
        response.error.errors = customError;
      }
    }

    if (response.status === 500) {
      customError.push(
        'Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.'
      );
      customResponse.error.errors = customError;
      return throwError(() => customResponse);
    }

    return throwError(() => response);
  }
}
