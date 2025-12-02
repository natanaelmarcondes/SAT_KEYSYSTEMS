using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.Data;

namespace SAT.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _db;

        public ProdutoController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string busca)
        {
            var lista = _db.Produtos
                .Where(x => busca == null || x.Descricao.Contains(busca) || x.Codigo.Contains(busca))
                .ToList();

            return View(lista);
        }
    }
}
