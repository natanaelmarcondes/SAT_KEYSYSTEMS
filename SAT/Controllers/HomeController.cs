using Microsoft.AspNetCore.Mvc;
using SAT.Data;
using SAT.Models;

namespace SAT.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public HomeController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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
                                .Where(p =>
                                    Convert.ToInt32(p.prd_Codigo) == codDigitado).Take(10)
                                .ToList();
                        }
                        else
                        {
                            lista = new List<Produto>();
                        }
                        break;
                    default:
                        lista = _context.Produtos
                            .Where(p =>
                                p.prd_Descri.Contains(termo)).Take(10)
                            .ToList();
                        break;
                }
            }

            // Verificar anexos (JPG ou PDF)
            string basePath = _config["DiretorioAnexos"] ?? "";

            if (!string.IsNullOrEmpty(basePath))
            {
                foreach (var item in lista)
                {
                    string png = Path.Combine(basePath, $"{item.prd_NomImg}");
                    //string pdf = Path.Combine(basePath, $"{item.prd_NomImg}.pdf");

                    item.TemAnexo = (System.IO.File.Exists(png) );
                }
            }

            // ----------------------------------------------

            ViewBag.TipoBusca = tipoBusca;
            ViewBag.Termo = termo;

            return View(lista);
        }
    }
}
