using Microsoft.AspNetCore.Mvc;

namespace SAT.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public ProdutosController(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public IActionResult Anexo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return Content("Código inválido.");

            string basePath = _config["DiretorioAnexos"];

            // Arquivos possíveis
            string pngPath = Path.Combine(basePath, $"{codigo}");
            //string pdfPath = Path.Combine(basePath, $"{codigo}.pdf");

            if (System.IO.File.Exists(pngPath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(pngPath);
                return File(fileBytes, "image/png");
            }

            //if (System.IO.File.Exists(pdfPath))
            //{
            //    byte[] fileBytes = System.IO.File.ReadAllBytes(pdfPath);
            //    return File(fileBytes, "application/pdf");
            //}

            return Content("Nenhum anexo encontrado para este produto.");
        }
        public IActionResult Visualizar(string codigo)
        {
            ViewBag.Codigo = codigo;
            return View();
        }

    }
}
