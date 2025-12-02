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
        [Authorize(Policy = "Admin")]
        public IActionResult Novo(Usuario u)
        {
            // senha em u.SenhaHash vem como texto puro aqui; gerar hash
            u.SenhaHash = BCrypt.Net.BCrypt.HashPassword(u.SenhaHash);
            _db.Usuarios.Add(u);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Edit(int id)
        {
            var usuario = _db.Usuarios.Find(id);
            if (usuario == null) return NotFound();
            // Não enviar o hash para a view em campo visível
            ViewBag.SenhaVazia = true;
            return View(usuario);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult Edit(int id, Usuario u, string novaSenha)
        {
            var usuario = _db.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            usuario.Nome = u.Nome;
            usuario.Email = u.Email;
            usuario.Perfil = u.Perfil;

            if (!string.IsNullOrWhiteSpace(novaSenha))
            {
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
            }

            _db.Usuarios.Update(usuario);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            var usuario = _db.Usuarios.Find(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var usuario = _db.Usuarios.Find(id);
            if (usuario == null) return NotFound();
            _db.Usuarios.Remove(usuario);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
