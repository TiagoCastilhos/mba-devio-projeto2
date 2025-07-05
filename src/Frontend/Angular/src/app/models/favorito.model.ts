import { Produto } from './produto.model';

export type Favorito = {
  id: string;
  produto: Produto;
  clienteId: string;
  produtoId: string;
};
