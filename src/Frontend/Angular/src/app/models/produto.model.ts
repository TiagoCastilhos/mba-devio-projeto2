export type Produto = {
  id: string;
  nome: string;
  descricao: string;
  preco: number;
  estoque: number;
  imagem: string;
  categoria: string;
  vendedor: string;
  categoriaId: string;
  vendedorId: string;
  favoritoId: string | null;
  produtosVendedor: Produto[];
};
