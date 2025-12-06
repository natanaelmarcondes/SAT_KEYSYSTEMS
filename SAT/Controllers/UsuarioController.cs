using Microsoft.AspNetCore.Mvc;
using SAT.Models;
using SAT.Data;

namespace SAT.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Usuarios.ToList();
            return View(lista);
        }

        [HttpPost]
        public IActionResult Create(Usuario u)
        {
            u.SenhaHash = BCrypt.Net.BCrypt.HashPassword("1234");
            _context.Usuarios.Add(u);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Usuario u)
        {
            var original = _context.Usuarios.Find(u.Id);
            if (original == null) return NotFound();

            original.Nome = u.Nome;
            original.Email = u.Email;
            original.Perfil = u.Perfil;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var u = _context.Usuarios.Find(id);
            if (u != null)
            {
                _context.Usuarios.Remove(u);
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult ResetarSenha(int id)
        {
            var u = _context.Usuarios.Find(id);
            if (u == null) return NotFound();

            u.SenhaHash = BCrypt.Net.BCrypt.HashPassword("1234");
            _context.SaveChanges();

            return Ok();
        }
    }
}
