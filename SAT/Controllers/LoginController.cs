using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SAT.Data;
using SAT.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace SAT.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        private readonly ILogger<LoginController> _logger;

        public LoginController(AppDbContext db, IConfiguration config, ILogger<LoginController> logger)
        {
            _db = db;
            _config = config;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(string email, string senha)
        {
            _logger.LogInformation("Entrar: início do fluxo de login");
            _logger.LogInformation("Entrar: email recebido='{EmailRaw}'", email);
            // 1️⃣ Testa a conexão antes de consultar o banco
            string erroConexao;
            string connString = _config.GetConnectionString("DefaultConnection");

            if (!DatabaseHelper.TestarConexao(connString, out erroConexao))
            {
                _logger.LogError("Banco de dados indisponível: {Erro}", erroConexao);
                ViewBag.Erro = "Banco de dados indisponível: " + erroConexao;
                return View("Index");
            }

            // 2️⃣ Validações e diagnóstico melhorados
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                _logger.LogWarning("Email ou senha vazios");
                ViewBag.Erro = "Email e senha são obrigatórios.";
                return View("Index");
            }

            var emailNormalized = email.Trim().ToLower();
            _logger.LogInformation("Email normalizado: {EmailNormalized}", emailNormalized);
            var usuario = _db.Usuarios.FirstOrDefault(x => x.Email.ToLower() == emailNormalized);

            if (usuario == null)
            {
                _logger.LogWarning("Usuário não encontrado para email {Email}", emailNormalized);
                ViewBag.Erro = "Usuário não encontrado.";
                return View("Index");
            }

            if (string.IsNullOrEmpty(usuario.SenhaHash))
            {
                _logger.LogWarning("Usuário {UserId} sem senha cadastrada", usuario.Id);
                ViewBag.Erro = "Usuário sem senha cadastrada.";
                return View("Index");
            }

            bool senhaValida;
            try
            {
                _logger.LogInformation("Hash do usuário (raw)='{HashRaw}' length={HashLen}", usuario.SenhaHash, usuario.SenhaHash?.Length ?? 0);
                senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash);
                _logger.LogInformation("Resultado BCrypt.Verify para user {UserId}: {Resultado}", usuario.Id, senhaValida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar senha para usuário {UserId}", usuario.Id);
                ViewBag.Erro = "Erro ao validar senha: " + ex.Message;
                return View("Index");
            }

            if (!senhaValida)
            {
                ViewBag.Erro = "Senha incorreta!";
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
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index");
        }
    }
}
