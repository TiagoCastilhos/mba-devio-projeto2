using Microsoft.AspNetCore.Mvc;
using XpertStore.Api.Controllers.Base;
using XpertStore.Api.Models;
using XpertStore.Data.Models;
using XpertStore.Data.Repositories;

namespace XpertStore.Api.Controllers;

public class CategoriasController : BaseApiController
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriasController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> GetAll()
    {
        IEnumerable<Categoria> result = await _categoriaRepository.GetAllAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CategoriaViewModel>> Get(Guid id)
    {
        var model = await _categoriaRepository.GetByIdAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        var viewModel = new CategoriaViewModel
        {
            Id = model.Id,
            Nome = model.Nome,
            Descricao = model.Descricao,
        };

        return Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CategoriaViewModel>> Post(CategoriaViewModel categoriaViewModel)
    {
        if (_categoriaRepository.IsNull())
        {
            return Problem("Erro ao criar, contate o suporte!");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        await _categoriaRepository.CreateAsync(new Categoria
        {
            Nome = categoriaViewModel.Nome!,
            Descricao = categoriaViewModel.Descricao!,
        });

        return CreatedAtAction(nameof(Get), new { id = categoriaViewModel.Id }, categoriaViewModel);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(Guid id, CategoriaViewModel categoriaViewModel)
    {
        if (id != categoriaViewModel.Id) return BadRequest();

        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        await _categoriaRepository.UpdateAsync(new Categoria
        {
            Id = id,
            Nome = categoriaViewModel.Nome!,
            Descricao = categoriaViewModel.Descricao!,
        });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (_categoriaRepository.IsNull())
        {
            return NotFound();
        }

        if (await _categoriaRepository.CategoriaEmUsoAsync(id))
        {
            return Problem("Categoria com Produto associado");
        }

        var categoria = await _categoriaRepository.DeleteAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
