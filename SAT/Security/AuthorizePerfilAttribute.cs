using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SAT.Security
{
    public class AuthorizePerfilAttribute : ActionFilterAttribute
    {
        private readonly string _perfilPermitido;

        public AuthorizePerfilAttribute(string perfil)
        {
            _perfilPermitido = perfil;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            string? perfilUsuario = session.GetString("UsuarioPerfil");

            if (perfilUsuario == null)
            {
                // Não está logado → redireciona para login
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            if (!perfilUsuario.Equals(_perfilPermitido, StringComparison.OrdinalIgnoreCase))
            {
                // Está logado, mas sem permissão → exibe página de acesso negado
                context.Result = new ContentResult
                {
                    Content = "Acesso negado. Você não tem permissão para acessar este módulo.",
                    StatusCode = 403
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
