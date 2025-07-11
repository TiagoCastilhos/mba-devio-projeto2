using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Application.Mappings
{
    public static class EntityMapping
    {
        #region Categoria
        public static IEnumerable<CategoriaViewModel> MapToListCategoriaViewModel(IEnumerable<Categoria> categorias)
        {
            if (categorias is null || !categorias.Any())
                return [];

            return [.. categorias.Select(MapToCategoriaViewModel)];
        }

        public static CategoriaViewModel MapToCategoriaViewModel(Categoria categoria)
        {
            if (categoria is null) return null;

            return new CategoriaViewModel
            {
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = categoria.Ativo,
                Id = categoria.Id,
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
                list.Add(MapToProdutoViewModel(produto));

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
                ImagemDisplay = produto.Imagem[37..],                
                CategoriaId = produto.CategoriaId,
                VendedorId = produto.VendedorId,
                Ativo = produto.Ativo,
                Categoria = produto.Categoria.Nome,
                Vendedor = produto.Vendedor.Nome
            };
        }

        public static Produto MapToProduto(ProdutoViewModel produto)
        {
            return new(produto.Id, produto.Estoque, produto.Preco, produto.Nome, produto.Descricao, produto.Imagem, produto.CategoriaId, produto.VendedorId, produto.Ativo, produto.FileUpload);
        }
        #endregion

        #region Vendedor
        public static IEnumerable<VendedorViewModel> MapToListVendedorViewModel(IEnumerable<Vendedor> vendedores)
        {
            List<VendedorViewModel> list = [];

            foreach (var vendedor in vendedores)
            {
                list.Add(MapToVendedorViewModel(vendedor));
            }

            return list;
        }

        public static VendedorViewModel MapToVendedorViewModel(Vendedor vendedor)
        {
            return new VendedorViewModel
            {
                Id = vendedor.Id,
                Nome = vendedor.Nome,
                Email = vendedor.Email,
                QuantidadeProdutos = (vendedor.Produto?.Any()).GetValueOrDefault() ? vendedor.Produto.Count() : 0,
                Ativo = vendedor.Ativo,
                Senha = vendedor.Senha
            };
        }

        public static Vendedor MapToVendedor(VendedorViewModel vendedor)
        {
            return new(vendedor.Id, vendedor.Nome, vendedor.Email, vendedor.Senha, vendedor.Ativo);
        }
        #endregion

        #region Cliente
        //TODO
        #endregion

    }
}
