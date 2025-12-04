using Microsoft.AspNetCore.Mvc;

namespace SAT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
