
using Aplication.CommandsQueries.Mesas_CommandsQueries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apiReservas.Controllers
{
    [ApiExplorerSettings(GroupName = "principal")]
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MesasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ListaMesas")]
        public async Task<IActionResult> ListaMesas()
        {
            string message = "Lista Mesas";
            var data = await _mediator.Send(new ListMesasQuery());
            return new OkObjectResult(new { message, data });
        }

        [HttpGet("ListaMesasxZona/{zona_id}")]
        public async Task<IActionResult> ListaMesasxZona(int zona_id)
        {
            string message = "Lista mesas x Zona";
            var data = await _mediator.Send(new ListMesasxZonaQuery() { zona_id=zona_id});
            return new OkObjectResult(new { message, data });
        }

        [HttpGet("DetalleMesa/{id}")]
        public async Task<IActionResult> DetalleMesa(int id)
        {
            string message = "Detalle";
            var data = await _mediator.Send(new DetalleMesaQuery() { MesaId = id });
            return new OkObjectResult(new { message, data });
        }

        [HttpPost("CrearMesa")]
        public async Task<IActionResult> CrearMesa([FromBody] Mesas reserva)
        {
            ServiceResponse respuesta = new ServiceResponse();
            if(reserva == null)
            {
                respuesta.message = "No se envio Data";
                respuesta.response = false;
                return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
            }
            var command = new CreateMesasCommand() { CreateMesa = reserva };
            respuesta = await _mediator.Send(command);
            return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
        }

        [HttpPost("UpdateMesa")]
        public async Task<IActionResult> UpdateMesa([FromBody] Mesas reserva)
        {
            ServiceResponse respuesta = new ServiceResponse();
            if(reserva == null)
            {
                respuesta.message = "No se envio Data";
                respuesta.response = false;
                return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
            }
            var command = new UpdateMesasCommand() { UpdateMesa = reserva };
            respuesta = await _mediator.Send(command);
            return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
        }

    }
}
