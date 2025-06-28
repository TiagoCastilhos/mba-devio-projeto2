export interface Produto {
  id: string;
  nome: string;
  descricao: string;
  preco: number;
  estoque: number;
  imagem: string;
  categoria: string;
  categoriaId: string;
  vendedor: string;
  favorito: boolean; //rever
}
