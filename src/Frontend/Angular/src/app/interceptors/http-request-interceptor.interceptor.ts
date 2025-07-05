import { HttpErrorResponse, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';
import { AuthenticationService } from '@services/authentication.service';
import { catchError, EMPTY, throwError } from 'rxjs';

export const httpRequestInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  if (!req.url.startsWith(environment.apiUrl)) {
    return next(req);
  }

  const authService = inject(AuthenticationService);
  const router = inject(Router);

  const authToken = authService.obterTokenUsuario();

  if (!authToken) {
    return next(req).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          router.navigate(['/auth/login']);
          return EMPTY;
        }

        return throwError(() => error);
      })
    );
  }

  const clonedRequest = req.clone({
    setHeaders: {
      'Authorization': `Bearer ${authToken}`
    }
  });

  return next(clonedRequest).pipe(
    catchError(error => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        router.navigate(['/auth/login']);
        return EMPTY;
      }

      return throwError(() => error);
    })
  );
};
