using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Application.Mappings
{
    public static class EntityMapping
    {
        #region Categoria
        public static IEnumerable<CategoriaViewModel> MapToListCategoriaViewModel(IEnumerable<Categoria> categorias)
        {
            List<CategoriaViewModel> list = [];

            foreach (var categoria in categorias)
                list.Add(MapToCategoriaViewModel(categoria));

            return list;
        }

        public static CategoriaViewModel MapToCategoriaViewModel(Categoria categoria)
        {
            return new CategoriaViewModel
            {
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = categoria.Ativo,
                QuantidadeProdutos = (categoria.Produto?.Any()).GetValueOrDefault() ? categoria.Produto.Count() : 0
            };
        }

        public static Categoria MapToCategoria(CategoriaViewModel categoria)
        {
            return new(categoria.Id, categoria.Nome, categoria.Descricao, categoria.Ativo);
        }
        #endregion

        #region Produto
        public static IEnumerable<ProdutoViewModel> MapToListProdutoViewModel(IEnumerable<Produto> produtos)
        {
            List<ProdutoViewModel> list = [];

            foreach (var produto in produtos)
            {
                list.Add(MapToProdutoViewModel(produto));
            }

            return list;
        }

        public static ProdutoViewModel MapToProdutoViewModel(Produto produto)
        {
            return new ProdutoViewModel
            {
                Id = produto.Id,
                Estoque = produto.Estoque,
                Preco = produto.Preco,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Imagem = produto.Imagem,
                CategoriaId = produto.CategoriaId,
                VendedorId = produto.VendedorId,
                Ativo = produto.Ativo,
                Categoria = produto.Categoria.Nome
            };
        }

        public static Produto MapToProduto(ProdutoViewModel produto)
        {
            return new(produto.Id, produto.Estoque, produto.Preco, produto.Nome, produto.Descricao, produto.Imagem, produto.CategoriaId, produto.VendedorId, produto.Ativo, produto.FileUpload);
        }
        #endregion

        #region Vendedor
        //TODO
        #endregion

        #region Cliente
        //TODO
        #endregion

    }
}
