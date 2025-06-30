import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BaseButtonComponent } from '../../components/base-button/base-button.component';
import { DetalhesProdutoComponent } from './detalhes-produto/detalhes-produto.component';
import { ListaProdutoComponent } from './lista-produto/lista-produto.component';
import { produtosRoutes } from './produtos.routes';
import { ProdutosService } from './services/produtos/produtos.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FavoritosService } from '../../services/favoritos.service';

@NgModule({
  declarations: [ListaProdutoComponent, DetalhesProdutoComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(produtosRoutes),
    BaseButtonComponent,
    FontAwesomeModule
  ],
  providers: [ProdutosService, FavoritosService],
})
export class ProdutosModule { }
