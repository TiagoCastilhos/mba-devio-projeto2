using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpertStore.Data.Models;
using XpertStore.Data.Repositories;
using XpertStore.Mvc.Models;

namespace XpertStore.Mvc.Controllers;

[Authorize]
public class CategoriasController : Controller
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriasController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _categoriaRepository.GetAllAsync());
    }

    public async Task<IActionResult> Details(Guid id)
    {
        if (_categoriaRepository.IsNull())
        {
            return NotFound();
        }

        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] CategoriaViewModel categoriaViewModel)
    {
        if (ModelState.IsValid)
        {
            Categoria categoria = new Categoria
            {
                Nome = categoriaViewModel.Nome,
                Descricao = categoriaViewModel.Descricao,
            };

            await _categoriaRepository.CreateAsync(categoria);
            return RedirectToAction(nameof(Index));
        }
        return View(categoriaViewModel);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        if (_categoriaRepository.IsNull())
        {
            return NotFound();
        }

        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        CategoriaViewModel categoriaViewModel = new()
        {
            Descricao = categoria.Descricao,
            Nome = categoria.Nome,
        };

        return View(categoriaViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Descricao")] CategoriaViewModel categoriaViewModel)
    {
        if (id != categoriaViewModel.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var categoria = await _categoriaRepository.GetByIdAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }

                categoria.Nome = categoriaViewModel.Nome;
                categoria.Descricao = categoriaViewModel.Descricao;

                await _categoriaRepository.UpdateAsync(categoria);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(categoriaViewModel);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        if (_categoriaRepository.IsNull())
        {
            return NotFound();
        }

        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);

        if (await _categoriaRepository.CategoriaEmUsoAsync(id))
        {
            return BadRequest("Categoria com Produto associado");
        }

        if (categoria != null)
        {
            await _categoriaRepository.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }
}
