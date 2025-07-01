export type Produto = {
  id: string;
  nome: string;
  descricao: string;
  preco: number;
  estoque: number;
  imagem: string;
  categoria: string;
  categoriaId: string;
  vendedorId: string;
  favorito: boolean; //rever
};
