import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BaseButtonComponent } from '@components/base-button/base-button.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FavoritosService } from '@services/favoritos.service';
import { DetalhesProdutoComponent } from './detalhes-produto/detalhes-produto.component';
import { ListaProdutoComponent } from './lista-produto/lista-produto.component';
import { produtosRoutes } from './produtos.routes';

@NgModule({
  declarations: [ListaProdutoComponent, DetalhesProdutoComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(produtosRoutes),
    BaseButtonComponent,
    FontAwesomeModule
  ],
  providers: [FavoritosService],
})
export class ProdutosModule { }
