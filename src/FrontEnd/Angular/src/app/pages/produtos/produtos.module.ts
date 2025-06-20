import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { produtosRoutes } from './produtos.routes';
import { ProdutosService } from './services/produtos/produtos.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(produtosRoutes)
  ],
  providers: [
    ProdutosService
  ]
})
export class ProdutosModule { }
