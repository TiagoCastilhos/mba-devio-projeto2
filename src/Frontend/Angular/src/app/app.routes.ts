import { Routes } from '@angular/router';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { FavoritosComponent } from './pages/favoritos/favoritos.component';

export const routes: Routes = [
    { path: 'produtos', loadChildren: () => import('./pages/produtos/produtos.module').then(m => m.ProdutosModule) },
    { path: 'auth', loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule) },
    { path: 'favoritos', component: FavoritosComponent },
    { path: '', redirectTo: 'produtos', pathMatch: 'full' },
    { path: '**', component: NotFoundComponent },
];
