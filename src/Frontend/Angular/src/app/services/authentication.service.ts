import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  constructor() { }

  login(email: string, password: string) {
    return this.http.post<{ success: boolean, data: string }>(
      `${this.apiUrl}/Auth/login`,
      {
        email: email,
        password: password
      }
    ).pipe(tap({
      next: (response) => {
        {
          if (response.success) {
            sessionStorage.setItem('access_token', response.data);
          } else {
            sessionStorage.removeItem('access_token');
          }
        }
      }
    }));
  }

  getAuthToken() {
    const token = sessionStorage.getItem('access_token');
    return token;
  }
}
