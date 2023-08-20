
using Aplication.CommandsQueries.Zonas_CommandsQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace apiReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ZonasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ListaZonas")]
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
       
    }
}
