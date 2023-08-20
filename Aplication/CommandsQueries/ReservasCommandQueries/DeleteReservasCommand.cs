
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.ReservasCommandQueries
{
    public class DeleteReservasCommand : IRequest<bool>
    {
        public int Reserva_id { get; set; }
    }
}
