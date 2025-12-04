using Microsoft.AspNetCore.Mvc;
using SAT.Data;
using SAT.Models;

namespace SAT.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string tipoBusca, string termo)
        {
            // Proteção: só entra se estiver logado
            if (HttpContext.Session.GetString("UsuarioNome") == null)
                return RedirectToAction("Index", "Login");

            IEnumerable<Produto> lista = Enumerable.Empty<Produto>();

            if (!string.IsNullOrEmpty(termo))
            {
                switch (tipoBusca)
                {
                    case "codigo":
                        if (int.TryParse(termo, out int codDigitado))
                        {
                            lista = _context.Produtos
                                .Where(p => Convert.ToInt32(p.prd_Codigo) == codDigitado && p.emp_Codigo == "01" && p.mrc_Codigo == "01")
                                .ToList();
                        }
                        else
                        {
                            lista = new List<Produto>();
                        }
                        break;


                    case "codigobarras":
                        if (decimal.TryParse(termo, out decimal codBarras))
                        {
                            lista = _context.Produtos
                                .Where(p => p.prd_CodBar == codBarras && p.emp_Codigo == "01" && p.mrc_Codigo == "01")
                                .ToList();
                        }
                        else
                        {
                            lista = new List<Produto>();
                        }
                        break;


                    default:
                        lista = _context.Produtos
                            .Where(p => p.prd_Descri.Contains(termo) && p.emp_Codigo == "01" && p.mrc_Codigo == "01")
                            .ToList();
                        break;
                }
            }

            ViewBag.TipoBusca = tipoBusca;
            ViewBag.Termo = termo;
            return View(lista);
        }
    }
}
