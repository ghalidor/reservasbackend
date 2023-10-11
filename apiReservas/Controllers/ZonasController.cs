
using apiReservas.Seguridad;
using Aplication.CommandsQueries.Zonas_CommandsQueries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apiReservas.Controllers
{
    [ApiExplorerSettings(GroupName = "principal")]
    [Route("api/[controller]")]
    [ApiController]
    public class ZonasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ZonasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("ListaZonas")]
        [ApiExplorerSettings(GroupName = "mantenimiento")]
        public async Task<IActionResult> ListaZonas()
        {
            string message = "Lista Zonas";
            var data = await _mediator.Send(new ListZonasQuery());
            return new OkObjectResult(new { message, data });
        }

        [HttpGet("ListaMesasAsignadas")]
        public async Task<IActionResult> ListaMesasxZonas()
        {
            string message = "Lista Zonas con Mesas Asignadas";
            var data = await _mediator.Send(new ListZonasMesasAsignadasQuery());
            return new OkObjectResult(new { message, data });
        }

        [HttpGet("DetalleZona/{id}")]
        public async Task<IActionResult> DetalleZona(int id)
        {
            string message = "Detalle";
            var data = await _mediator.Send(new DetalleZonaQuery() { ZonaId = id });
            return new OkObjectResult(new { message, data });
        }

        [HttpPost("CrearZona")]
        public async Task<IActionResult> CrearZona([FromBody] Zonas reserva)
        {
            ServiceResponse respuesta = new ServiceResponse();
            if(reserva == null)
            {
                respuesta.message = "No se envio Data";
                respuesta.response = false;
                return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
            }
            var command = new CreateZonaCommand() { CreateZona = reserva };
            respuesta = await _mediator.Send(command);
            return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
        }

        [HttpPost("UpdateZona")]
        public async Task<IActionResult> UpdateZona([FromBody] Zonas reserva)
        {
            ServiceResponse respuesta = new ServiceResponse();         
            var command = new UpdateZonaCommand() { UpdateZona = reserva };
            respuesta = await _mediator.Send(command);
            return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
        }

    }
}
