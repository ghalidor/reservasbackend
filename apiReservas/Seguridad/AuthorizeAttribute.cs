using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace apiReservas.Seguridad
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if(allowAnonymous)
                return;

            // authorization
            var user = (UsuarioResponse)context.HttpContext.Items["User"];
            if(user == null)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            // skip authorization if action is decorated with [AllowAnonymous] attribute

            var user = (UsuarioResponse)context.HttpContext.Items["User"];
            var timeExpired = (bool)context.HttpContext.Items["expired"];
            Int64 id_usuario = 0;
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if(allowAnonymous)
            {
                return;

            }
            else
            {
                if(user == null)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized,Inicia Sesión" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
                else
                {
                    if(timeExpired)
                    {
                        context.Result = new JsonResult(new { message = "Sesion TimeOut,Vuelve a iniciar Sesión" }) { StatusCode = StatusCodes.Status419AuthenticationTimeout };
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void SetResultUnAuthorize(AuthorizationFilterContext context)
        {
            context.Result = (IActionResult)new UnauthorizedResult();
        }
    }
}
