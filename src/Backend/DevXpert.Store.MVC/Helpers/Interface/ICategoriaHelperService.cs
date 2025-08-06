using DevXpert.Store.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevXpert.Store.MVC.Helpers.Interface
{
    public interface ICategoriaHelperService
    {
        Task<IEnumerable<CategoriaViewModel>> BuscarCategoriasAsync();
        Task<List<SelectListItem>> GetCategoriasFilterAsync(Guid? selected);
    }

}
