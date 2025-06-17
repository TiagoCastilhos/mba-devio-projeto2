using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpertStore.Api.Controllers.Base;
using XpertStore.Api.Extensions;
using XpertStore.Api.Models;
using XpertStore.Data.Models;
using XpertStore.Data.Models.Base;
using XpertStore.Data.Repositories;

namespace XpertStore.Api.Controllers;

public class ProdutosController : BaseApiController
{
    private readonly IProdutoRepository _produtoRepository;

    protected Guid? UserId { get; set; } = null;

    public ProdutosController(
        IAppIdentityUser user,
        IProdutoRepository produtoRepository)
    {
        if (user.IsAuthenticated()) UserId = user.GetUserId();
        _produtoRepository = produtoRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Get(string? categoria, Guid? categoriaId)
    {
        if (_produtoRepository.IsNull())
        {
            return NotFound();
        }

        var produtos = await _produtoRepository.GetProdutosCategoriaVendedorAsync();

        if (!string.IsNullOrEmpty(categoria))
            produtos = produtos.Where(p => p.Categoria.Descricao.Contains(categoria));

        if (categoriaId != null)
            produtos = produtos.Where(p => p.Categoria.Id == categoriaId);

        var produtosViewModel = MapToListProdutosViewModel(produtos);

        return Ok(produtosViewModel);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ProdutoViewModel>> Get(Guid id)
    {
        if (_produtoRepository.IsNull())
        {
            return NotFound();
        }

        var produto = await GetByIdAsync(id);

        if (produto == null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Post(ProdutoViewModel produtoViewModel)
    {
        if (_produtoRepository.IsNull())
        {
            return Problem("Erro ao criar um produto, contate o suporte!");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });
        }

        var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;

        if (!FileExtension.UploadArquivoBase64(produtoViewModel.ImagemUpload, imagemNome, ModelState))
        {
            return BadRequest(produtoViewModel);
        }

        var produto = new Produto
        {
            Nome = produtoViewModel.Nome,
            Descricao = produtoViewModel.Descricao,
            Preco = produtoViewModel.Preco,
            Estoque = produtoViewModel.Estoque,
            CategoriaId = produtoViewModel.CategoriaId,
            VendedorId = UserId.Value,
            Imagem = imagemNome
        };

        var result = await _produtoRepository.CreateAsync(produto);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, produtoViewModel);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put(Guid id, ProdutoViewModel produtoViewModel)
    {
        if (id != produtoViewModel.Id) return BadRequest();

        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var produto = await GetByIdAsync(id);

        if (produto.Vendedor.Id != UserId)
        {
            return Unauthorized();
        }

        if (produtoViewModel.ImagemUpload != null)
        {
            var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;
            if (!FileExtension.UploadArquivoBase64(produtoViewModel.ImagemUpload, imagemNome, ModelState))
            {
                return BadRequest(produtoViewModel);
            }
            produto.Imagem = imagemNome;
        }

        produto.Nome = produtoViewModel.Nome;
        produto.Descricao = produtoViewModel.Descricao;
        produto.Preco = produtoViewModel.Preco;
        produto.Estoque = produtoViewModel.Estoque;
        produto.CategoriaId = produtoViewModel.CategoriaId;

        try
        {
            await _produtoRepository.UpdateAsync(produto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_produtoRepository.Exists(id))
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
    public async Task<IActionResult> Delete(Guid id)
    {
        if (_produtoRepository.IsNull())
        {
            return NotFound();
        }

        var result = await _produtoRepository.DeleteAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    private async Task<Produto> GetByIdAsync(Guid id)
    {
        return await _produtoRepository.GetProdutoCategoriaVendedorByIdAndUserIdAsync(id);
    }

    private IEnumerable<ProdutoViewModel> MapToListProdutosViewModel(IEnumerable<Produto> produtos)
    {
        var produtoViewModelList = new List<ProdutoViewModel>();

        foreach (var produto in produtos)
        {
            produtoViewModelList.Add(MapToProdutoViewModel(produto));
        }

        return produtoViewModelList;
    }

    private ProdutoViewModel MapToProdutoViewModel(Produto produto)
    {
        return new ProdutoViewModel
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Imagem = produto.Imagem,
            Preco = produto.Preco,
            Estoque = produto.Estoque,
            CategoriaId = produto.CategoriaId,
            Categoria = MapToCategoriaViewModel(produto.Categoria)
        };
    }

    private static CategoriaViewModel MapToCategoriaViewModel(Categoria categoria)
    {
        return new CategoriaViewModel()
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
        };
    }


}
