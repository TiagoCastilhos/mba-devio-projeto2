import { Routes } from '@angular/router';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { DetalhesProdutoComponent } from './pages/produtos/detalhes-produto/detalhes-produto.component';
import { ListaProdutoComponent } from './pages/produtos/lista-produto/lista-produto.component';

export const routes: Routes = [
    { path: 'produtos', component: ListaProdutoComponent },
    { path: 'produtos/:id', component: DetalhesProdutoComponent },
    { path: '', redirectTo: 'produtos', pathMatch: 'full' },
    { path: '**', component: NotFoundComponent },
];
