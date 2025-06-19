import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { produtosRoutes } from './produtos.routes';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(produtosRoutes)
  ]
})
export class ProdutosModule { }
