using CadastrarProduto.Models;
using CadastrarProduto.Repository;
using Microsoft.AspNetCore.Mvc;
namespace CadastrarProduto.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoController(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public IActionResult Index()
        {
            return View(_produtoRepository.TodosOsProdutos());
        }
        public IActionResult CadastroProduto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.CadastrarProduto(produto);

                return RedirectToAction("Index");
            }
            return View(produto);
        }
    }
}
