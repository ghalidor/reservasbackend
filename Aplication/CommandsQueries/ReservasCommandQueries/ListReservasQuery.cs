using Domain;
using MediatR;

namespace Aplication.CommandsQueries.ReservasCommandQueries
{
    public class ListReservasQuery : IRequest<IEnumerable<Reservas>>
    {
        public string fechaini { get; set; }
        public string fechafin { get; set; }
    }
}
