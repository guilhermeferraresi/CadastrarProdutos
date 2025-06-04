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
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.AdicionarProduto(produto);

                //return RedirectToAction(" ");
            }
            return View(produto);
        }
    }
}
