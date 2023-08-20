using Aplication.CommandsQueries.Empresa_CommandQueries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apiReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpresaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("RegistroEmpresa")]
        public async Task<IActionResult> RegistroEmpresa()
        {
            string message = "Registro Empresa";
            var data = await _mediator.Send(new RegistroEmpresaQuery());
            return new OkObjectResult(new { message, data });
        }

        [HttpPut("UpdateEmpresa")]
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
    }
}
