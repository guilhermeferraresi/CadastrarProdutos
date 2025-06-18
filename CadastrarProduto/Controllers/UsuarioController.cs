using CadastrarProduto.Models;
using CadastrarProduto.Repository;
using Microsoft.AspNetCore.Mvc;
namespace CadastrarProduto.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public IActionResult CadastroUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.AdicionarUsuario(usuario);
                
                return RedirectToAction("Login");
            }
            return View(usuario);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _usuarioRepository.ObterUsuario(email);
            if (usuario != null && usuario.Senha == senha)
            {
                return RedirectToAction("Index", "Produto");
            }

            ModelState.AddModelError("", "Email ou senha inválidos.");

            return View();
        }
        public IActionResult Contato()
        {
            return View();
        }

    }
}
