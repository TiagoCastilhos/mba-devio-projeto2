import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthResponse } from '@models/auth-response.model';
import { CustomResponse } from '@models/custom-response';
import { Usuario } from '@models/usuario.model';
import { tap } from 'rxjs';
import { BaseService } from './base.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends BaseService {
  private http = inject(HttpClient);
  private router = inject(Router);

  registrar(email: string, password: string) {
    return this.http.post<CustomResponse<string>>(
      `${this.apiUrl}/Auth/register`,
      {
        email: email,
        password: password,
        confirmPassword: password
      }
    ).pipe(tap({
      next: (response: AuthResponse) => {
        this.setarTokenUsuario(response);
      }
    }));
  }

  logar(email: string, password: string) {
    return this.http.post<CustomResponse<string>>(
      `${this.apiUrl}/Auth/login`,
      {
        email: email,
        password: password
      }
    ).pipe(tap({
      next: (response: AuthResponse) => {
        this.setarTokenUsuario(response);
        this.router.navigate(['']);
      }
    }));
  }

  obterTokenUsuario() {
    const token = sessionStorage.getItem('access_token');
    return token;
  }

  deslogar() {
    sessionStorage.removeItem('access_token');
    this.router.navigate(['']);
  }

  estaLogado(): boolean {
    const token = this.obterTokenUsuario();

    if (!token) {
      return false;
    }

    if (this.tokenExpirado(token)) {
      this.deslogar();
      return false;
    }

    return true;
  }

  obterUsuario(): Usuario | undefined {
    const token = this.obterTokenUsuario();

    if (!token) {
      return undefined;
    }

    return this.obterUsuarioToken(token);
  }

  private tokenExpirado(token: string) {
    const expiry = this.obterUsuarioToken(token).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }

  private setarTokenUsuario(response: AuthResponse) {
    if (response.success) {
      sessionStorage.setItem('access_token', response.data);
    } else {
      sessionStorage.removeItem('access_token');
    }
  }

  private obterUsuarioToken(token: string): Usuario {
    const payload = JSON.parse(atob(token.split('.')[1]));

    return {
      nome: payload.unique_name,
      role: payload.role,
      exp: payload.exp,
    };
  }
}
