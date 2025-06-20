import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

export const httpRequestInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  if (!req.url.startsWith(environment.apiUrl)) {
    return next(req);
  }

  const authToken = inject(AuthenticationService).getAuthToken();

  if (!authToken) {
    return next(req);
  }

  const clonedRequest = req.clone({
    setHeaders: {
      'Authorization': `Bearer ${authToken}`
    }
  });

  return next(clonedRequest);
};
