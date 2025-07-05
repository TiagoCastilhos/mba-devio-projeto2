import { CurrencyPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { Favorito } from '@models/favorito.model';
import { FavoritosService } from '@services/favoritos.service';
import { ToasterService } from '@services/toaster.service';
import { ProdutosService } from '../produtos/services/produtos/produtos.service';

@Component({
  selector: 'app-favoritos',
  imports: [FontAwesomeModule, RouterLink, CurrencyPipe],
  templateUrl: './favoritos.component.html',
  providers: [ProdutosService, FavoritosService],
})
export class FavoritosComponent implements OnInit {
  _toasterService = inject(ToasterService);
  _favoritosService = inject(FavoritosService);
  favoritos: Favorito[] = [];
  faTrashCan = faTrashCan;

  ngOnInit(): void {
    this.obterFavoritos();
  }

  private obterFavoritos() {
    this._favoritosService.buscarTodos().subscribe({
      next: (res) => {
        this.favoritos = res.data;
      },
    });
  }

  removerFavorito(favoritoId: string) {
    this._favoritosService.deletar(favoritoId).subscribe({
      next: () => {
        this.favoritos = this.favoritos.filter(
          (favorito) => favorito.id !== favoritoId
        );
        this._toasterService.sucesso('Produto removido de favoritos!');
      }
    });
  }
}
