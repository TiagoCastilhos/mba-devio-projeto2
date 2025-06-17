using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using XpertStore.Data.Data;
using XpertStore.Mvc.Data;
using XpertStore.Mvc.Models;

namespace XpertStore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _context.Produtos.ToListAsync();
            List<ProdutoHomeViewModel> produtoViewModel = [];
            foreach (var produto in produtos)
            {
                produtoViewModel.Add(new ProdutoHomeViewModel
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    Imagem = produto.Imagem,
                });
            }

            return View(produtoViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
