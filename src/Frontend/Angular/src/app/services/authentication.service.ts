import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { AuthResponse } from '../models/auth-response.model';
import { CustomResponse } from '../models/custom-response';
import { User } from '../models/user.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends BaseService {
  private http = inject(HttpClient);

  register(email: string, password: string) {
    return this.http.post<CustomResponse<string>>(
      `${this.apiUrl}/Auth/register`,
      {
        email: email,
        password: password,
        confirmPassword: password
      }
    ).pipe(tap({
      next: (response: AuthResponse) => {
        this.setAuthToken(response);
      }
    }));
  }

  login(email: string, password: string) {
    return this.http.post<CustomResponse<string>>(
      `${this.apiUrl}/Auth/login`,
      {
        email: email,
        password: password
      }
    ).pipe(tap({
      next: (response: AuthResponse) => {
        this.setAuthToken(response);
      }
    }));
  }

  getAuthToken() {
    const token = sessionStorage.getItem('access_token');
    return token;
  }

  logout() {
    sessionStorage.removeItem('access_token');
  }

  isLoggedIn(): boolean {
    const token = this.getAuthToken();

    if (!token) {
      return false;
    }

    if (this.tokenExpired(token)) {
      this.logout();
      return false;
    }

    return true;
  }

  getUser(): User | undefined {
    const token = this.getAuthToken();

    if (!token) {
      return undefined;
    }

    return this.readToken(token);
  }

  private tokenExpired(token: string) {
    const expiry = this.readToken(token).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }

  private setAuthToken(response: AuthResponse) {
    if (response.success) {
      sessionStorage.setItem('access_token', response.data);
    } else {
      sessionStorage.removeItem('access_token');
    }
  }

  private readToken(token: string): User {
    const payload = JSON.parse(atob(token.split('.')[1]));

    return {
      uniqueName: payload.unique_name,
      role: payload.role,
      exp: payload.exp,
    };
  }
}
