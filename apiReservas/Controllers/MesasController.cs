
using Aplication.CommandsQueries.Mesas_CommandsQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apiReservas.Controllers
{
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

        [HttpGet("ListaMesasxZona")]
        public async Task<IActionResult> ListaMesasxZona(int zona_id)
        {
            string message = "Lista mesas x Zona";
            var data = await _mediator.Send(new ListMesasxZonaQuery() { zona_id=zona_id});
            return new OkObjectResult(new { message, data });
        }

    }
}
