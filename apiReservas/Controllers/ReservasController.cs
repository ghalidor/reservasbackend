
using Aplication.CommandsQueries.Mesas_CommandsQueries;
using Aplication.CommandsQueries.ReservasCommandQueries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apiReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ListarReservas/{fechaini}/{fechafin}")]
        public async Task<IActionResult> ListarReservas(string fechaini,string fechafin)
        {
            string message = "Lista Reservas";
            var data = await _mediator.Send(new ListReservasQuery() { fechaini=fechaini,fechafin=fechafin});
            return new OkObjectResult(new { message, data });
        }

        [HttpGet("ListarReservaHorasZonaMesaLibre/{fecha}")]
        public async Task<IActionResult> GetBitacoraBySala(string fecha)
        {
            ServiceResponseReserva respuesta = new ServiceResponseReserva();
            respuesta = await _mediator.Send(new ListHorasLibreReservasQuery() { fecha=fecha});
            return new OkObjectResult(new {data= respuesta });
        }

        [HttpPost("CrearReserva")]
        public async Task<IActionResult> CrearReserva([FromBody] ReservacionNuevo reserva)
        {
            ServiceResponse respuesta = new ServiceResponse();
            if (reserva == null)
            {
                respuesta.message = "No se envio Data";
                respuesta.response = false;
                return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
            }
            var command = new CreateReservasCommand() { NewReservas = reserva };
            respuesta = await _mediator.Send(command);
            return new OkObjectResult(new { message = respuesta.message, respuesta = respuesta.response });
        }

        [HttpPut("UpdateReservas")]
        public async Task<IActionResult> UpdateTurno([FromBody] Reservas reserva)
        {
            bool respuesta = false;
            string message = string.Empty;
            if (reserva == null)
            {
                message = "No se envio Data";
                return new OkObjectResult(new { message, respuesta });
            }
            var command = new UpdateReservasCommand() { UpdateReservas = reserva };
            respuesta = await _mediator.Send(command);
            message = respuesta ? "Actualizado Correctamente" : "No se puedo Actualizar, error";
            return new OkObjectResult(new { message, respuesta });
        }

        [HttpDelete("EliminarReserva/{id}")]
        public async Task<IActionResult> EliminarReserva(int id)
        {
            bool respuesta = false;
            string message = string.Empty;
            var command = new DeleteReservasCommand() { Reserva_id = id };
            respuesta = await _mediator.Send(command);
            message = respuesta ? "Eliminado Correctamente" : "No se puedo Eliminar, error";
            return new OkObjectResult(new { message, respuesta });
        }
    }
}
