import { Routes } from "@angular/router";
import { DetalhesProdutoComponent } from "./detalhes-produto/detalhes-produto.component";
import { ListaProdutoComponent } from "./lista-produto/lista-produto.component";

export const produtosRoutes: Routes = [
    { path: '', component: ListaProdutoComponent },
    { path: ':id', component: DetalhesProdutoComponent },
]