using apiReservas.Seguridad;
using Aplication.CommandsQueries.Empresa_CommandQueries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apiReservas.Controllers
{
    [ApiExplorerSettings(GroupName = "principal")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtUtils _jwtUtils;
        public EmpresaController(IMediator mediator, IJwtUtils jwtUtils)
        {
            _mediator = mediator;
            _jwtUtils = jwtUtils;
        }

        [AllowAnonymous]
        [HttpGet("RegistroEmpresa")]
        [ApiExplorerSettings(GroupName = "mantenimiento")]
        public async Task<IActionResult> RegistroEmpresa()
        {
            string message = "Registro Empresa";
            var data = await _mediator.Send(new RegistroEmpresaQuery());
            return new OkObjectResult(new { message, data });
        }

        [HttpPost("UpdateEmpresa")]
        public async Task<IActionResult> UpdateEmpresa([FromBody] Empresa empresa)
        {
            ServiceResponse respuesta = new ServiceResponse();
            if (empresa == null)
            {
                respuesta.message = "No se envio Data";
                respuesta.response = false;
                return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
            }
            var command = new UpdateEmpresaCommand() { UpdateEmpresa = empresa };
            respuesta = await _mediator.Send(command);
            return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
        }

        [AllowAnonymous]
        [HttpPost("Acceso")]
        public async Task<IActionResult> Acceso([FromBody] UsuarioLogin usuario)
        {
            bool respuesta = false;
            string message = "Acceso al Sistema";
            string token = string.Empty;
            if(usuario.Usuario == null || usuario.Usuario.Equals("") ||

                    usuario.Password == null || usuario.Password.Equals(""))
            {
                message = "Ingresar los datos solictiados";
                respuesta = false;
                return new OkObjectResult(new { message, respuesta });
            }
            var data = await _mediator.Send(new GetusuarioLoginQuery() { usuario = usuario });
            if(data != null)
            {
                if(data.UsuarioId>0)
                {
                    token = _jwtUtils.GenerateToken(data);
                    respuesta = true;
                }
                else
                {
                    message = "Usuario/Contraseña Incorrecta , no se encontro";
                    respuesta = false;
                }
            }
            else
            {
                message = "Usuario/Contraseña Incorrecta , no se encontro";
                respuesta = false;
                return new OkObjectResult(new { message, respuesta = respuesta });
            }


            return new OkObjectResult(new { message,  respuesta , token });
        }
    }
}
