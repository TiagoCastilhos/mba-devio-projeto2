using DevXpert.Store.Core.Application.Mappings;
using DevXpert.Store.Core.Application.ViewModels;
using DevXpert.Store.Core.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevXpert.Store.Core.Application.Helpers
{
    public interface ICategoriaHelperService
    {
        Task<IEnumerable<CategoriaViewModel>> BuscarCategoriasAsync();
        Task<List<SelectListItem>> GetCategoriasFilterAsync(Guid? selected);
    }

    public class CategoriaHelperService : ICategoriaHelperService
    {
        private readonly ICategoriaService categoriaService;

        public CategoriaHelperService(ICategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        public async Task<IEnumerable<CategoriaViewModel>> BuscarCategoriasAsync()
        {
            var categorias = await categoriaService.BuscarTodos(string.Empty, true);
            return categorias.Select(EntityMapping.MapToCategoriaViewModel);
        }

        public async Task<List<SelectListItem>> GetCategoriasFilterAsync(Guid? selected)
        {
            var categorias = await BuscarCategoriasAsync();

            var lista = new List<SelectListItem>
        {
            new() { Value = "", Text = "Todas as Categorias", Selected = false }
        };

            lista.AddRange(categorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nome,
                Selected = c.Id == selected
            }));

            return lista;
        }
    }

}
