using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XpertStore.Data.Models;
using XpertStore.Data.Models.Base;
using XpertStore.Data.Repositories;
using XpertStore.Mvc.Extensions;
using XpertStore.Mvc.Models;

namespace XpertStore.Mvc.Controllers;

[Authorize]
public class ProdutosController : Controller
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IVendedorRepository _vendedorRepository;
    private Guid UserId;

    public ProdutosController(
        IProdutoRepository produtoRepository,
        IAppIdentityUser user,
        ICategoriaRepository categoriaRepository,
        IVendedorRepository vendedorRepository)
    {
        if (user.IsAuthenticated()) UserId = user.GetUserId();
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
        _vendedorRepository = vendedorRepository;
    }

    public async Task<IActionResult> Index()
    {
        return base.View(MapToProdutosViewModel(await _produtoRepository.GetProdutosCategoriaVendedorAsync()));
    }

    private IEnumerable<ProdutoViewModel> MapToProdutosViewModel(IEnumerable<Produto> produtos)
    {
        var produtoViewModelList = new List<ProdutoViewModel>();

        foreach (var produto in produtos)
        {
            produtoViewModelList.Add(MapToProdutoViewModel(produto));
        }

        return produtoViewModelList;
    }

    public async Task<IActionResult> Details(Guid id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produtoViewModel = await GetProdutoViewModelById(id);

        if (produtoViewModel == null)
        {
            return NotFound();
        }

        return View(produtoViewModel);
    }

    public async Task<IActionResult> Create()
    {
        await ObterCategoriasViewBag();
        return View();
    }

    private async Task ObterCategoriasViewBag()
    {
        ViewBag.Categorias = (await _categoriaRepository.GetAllAsync())
            .Select(c => new SelectListItem()
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            })
            .ToList();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Descricao,ImagemUpload,Preco,Estoque,CategoriaId")] ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await produtoViewModel.ImagemUpload!.UploadArquivo(imgPrefixo, ModelState))
            {
                return View(produtoViewModel);
            }

            Produto produto = new Produto
            {
                Id = produtoViewModel.Id,
                Nome = produtoViewModel.Nome,
                Descricao = produtoViewModel.Descricao,
                Preco = produtoViewModel.Preco,
                Estoque = produtoViewModel.Estoque,
                CategoriaId = produtoViewModel.CategoriaId,
                VendedorId = UserId,
                Imagem = imgPrefixo + produtoViewModel.ImagemUpload!.FileName
            };

            await _produtoRepository.CreateAsync(produto);
            return RedirectToAction(nameof(Index));
        }
        return View(produtoViewModel);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produtoViewModel = await GetProdutoViewModelById(id.Value);
        if (produtoViewModel == null)
        {
            return NotFound();
        }

        await ObterCategoriasViewBag();
        return View(produtoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Descricao,Imagem,ImagemUpload,Preco,Estoque,CategoriaId")] ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var produto = await _produtoRepository.GetByIdAsync(id);
                
                produto.Nome = produtoViewModel.Nome;
                produto.Descricao = produtoViewModel.Descricao;
                produto.Preco = produtoViewModel.Preco;
                produto.Estoque = produtoViewModel.Estoque;
                produto.CategoriaId = produtoViewModel.CategoriaId;

                if (produtoViewModel.ImagemUpload != null)
                {
                    var imgPrefixo = Guid.NewGuid() + "_";
                    if (!await produtoViewModel.ImagemUpload.UploadArquivo(imgPrefixo, ModelState))
                    {
                        return View(produtoViewModel);
                    }

                    produto.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
                }

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
            return RedirectToAction(nameof(Index));
        }
        return View(produtoViewModel);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await GetProdutoViewModelById(id.Value);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var produto = await GetProdutoById(id);

        if (produto.VendedorId != UserId)
        {
            return RedirectToAction(nameof(Index));
        }

        if (produto != null)
        {
            await _produtoRepository.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<ProdutoViewModel> GetProdutoViewModelById(Guid id)
    {
        return MapToProdutoViewModel(await GetProdutoById(id));
    }

    private async Task<Produto> GetProdutoById(Guid id)
    {
        return await _produtoRepository.GetProdutoCategoriaVendedorByIdAndUserIdAsync(id);
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

    private CategoriaViewModel MapToCategoriaViewModel(Categoria categoria)
    {
        return new CategoriaViewModel
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        };
    }

    private async Task<Produto> MapToProduto(ProdutoViewModel produtoViewModel)
    {
        return new Produto
        {
            Id = produtoViewModel.Id,
            Nome = produtoViewModel.Nome,
            Descricao = produtoViewModel.Descricao,
            Preco = produtoViewModel.Preco,
            Estoque = produtoViewModel.Estoque,
            Imagem = produtoViewModel.Imagem,
            Categoria = await _categoriaRepository.GetByIdAsync(produtoViewModel.CategoriaId),
            CategoriaId = produtoViewModel.CategoriaId,
            Vendedor = await _vendedorRepository.GetByIdAsync(UserId),
            VendedorId = UserId,
        };
    }
}
