using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.Data;
using SAT.Models;

namespace SAT.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _db;

        public UsuarioController(AppDbContext db)
        {
            _db = db;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index()
        {
            return View(_db.Usuarios.ToList());
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Novo(Usuario u)
        {
            u.SenhaHash = BCrypt.Net.BCrypt.HashPassword(u.SenhaHash);
            _db.Usuarios.Add(u);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
