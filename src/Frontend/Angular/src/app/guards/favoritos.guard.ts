import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AutenticacaoService } from '@services/autenticacao.service';

@Injectable({
  providedIn: 'root'
})
export class FavoritosGuard implements CanActivate {

  authService = inject(AutenticacaoService);
  router = inject(Router);

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    return this.validarClaims(route);
  }

  protected validarClaims(routeAc: ActivatedRouteSnapshot): boolean {
    if (!this.authService.obterTokenUsuario()) {
      this.router.navigate(['/auth/login/']);
      return false;
    }
    return true;
  }
}
