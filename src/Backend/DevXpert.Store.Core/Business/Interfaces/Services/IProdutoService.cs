﻿using DevXpert.Store.Core.Business.Models;

namespace DevXpert.Store.Core.Business.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> BuscarTodos(string busca = "", Guid? vendedorId = null, Guid? categoriaId = null, bool? ativo = true);
        Task<Produto> BuscarPorId(Guid id);
        Task<bool> Adicionar(Produto produto);
        Task<bool> Atualizar(Produto produto);
        Task<bool> AlternarStatus(Guid id);
        Task<bool> Excluir(Guid id);
        Task<bool> ProcessarStatusEmLote(Guid vendedorId, bool ativo);
        Task Salvar();
    }
}