using System.Security.Claims;
using Application.Base;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class ApiBeforeActionFilter: DadosSessaoBase, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var identity = context.HttpContext.User.Identity as ClaimsIdentity;

        var usuarioLogadoId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        
        if (usuarioLogadoId != null)
            UsuarioLogadoId =
                Int16.Parse(usuarioLogadoId);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}