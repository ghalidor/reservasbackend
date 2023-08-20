
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.ReservasCommandQueries
{
    public class ListHorasLibreReservasQuery : IRequest<ServiceResponseReserva>
    {
        public string fecha { get; set; }
    }
}
