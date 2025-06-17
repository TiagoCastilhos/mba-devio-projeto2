using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpertStore.Data.Data;
using XpertStore.Data.Models.Base;

namespace XpertStore.Api.Controllers.Base;

public class BaseCrudController<T> : BaseApiController where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public BaseCrudController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<T>>> Get()
    {
        if (_context.Set<T>() == null)
        {
            return NotFound();
        }

        return await _context.Set<T>().ToListAsync();
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<T>> Get(Guid id)
    {
        if (_context.Set<T>() == null)
        {
            return NotFound();
        }

        var model = await _context.Set<T>().FindAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<T>> Post(T entry)
    {
        if (_context.Set<T>() == null)
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

        _context.Set<T>().Add(entry);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(Guid id, T entry)
    {
        if (id != entry.Id) return BadRequest();

        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        _context.Entry(entry).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ModelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        if (_context.Set<T>() == null)
        {
            return NotFound();
        }

        var model = await _context.Set<T>().FindAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        _context.Set<T>().Remove(model);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ModelExists(Guid id)
    {
        return (_context.Set<T>()?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
