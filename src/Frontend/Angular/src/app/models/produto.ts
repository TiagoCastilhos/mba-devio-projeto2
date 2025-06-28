import { Categoria } from './categoria';

export interface Produto {
  id: string;
  nome: string;
  descricao: string;
  preco: number;
  estoque: number;
  imagem: string;
  categoria: Categoria;
  categoriaId: string;
  favorito: boolean; //rever
}
