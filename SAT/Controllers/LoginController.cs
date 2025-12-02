using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SAT.Data;
using SAT.Utils;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace SAT.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        // 🔹 ESTE É O CONSTRUTOR DO CONTROLLER
        public LoginController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(string email, string senha)
        {
            // 1️⃣ Testa a conexão antes de consultar o banco
            string erroConexao;
            string connString = _config.GetConnectionString("DefaultConnection");

            if (!DatabaseHelper.TestarConexao(connString, out erroConexao))
            {
                ViewBag.Erro = "Banco de dados indisponível: " + erroConexao;
                return View("Index");
            }

            // 2️⃣ Validação normal
            var usuario = _db.Usuarios.FirstOrDefault(x => x.Email == email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            {
                ViewBag.Erro = "Credenciais inválidas!";
                return View("Index");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Perfil", usuario.Perfil)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Index", "Home");
        }

        

        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
