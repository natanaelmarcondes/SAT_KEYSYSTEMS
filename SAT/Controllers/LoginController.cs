using Microsoft.AspNetCore.Mvc;
using SAT.Data;
using BCrypt.Net;

namespace SAT.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        // Tela de Login
        public IActionResult Index()
        {
            return View();
        }

        // Autenticação
        [HttpPost]
        public IActionResult Entrar(string email, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email);

            if (usuario == null)
            {
                ViewBag.Erro = "Usuário não encontrado.";
                return View("Index");
            }

            bool senhaOK = BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash);

            if (!senhaOK)
            {
                ViewBag.Erro = "Senha incorreta.";
                return View("Index");
            }

            // Sessão
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetString("UsuarioPerfil", usuario.Perfil);

            return RedirectToAction("Index", "Home");
        }

        // Logout
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
