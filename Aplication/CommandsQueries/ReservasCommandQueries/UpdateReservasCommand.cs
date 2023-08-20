using Domain;
using MediatR;

namespace Aplication.CommandsQueries.ReservasCommandQueries
{
    public class UpdateReservasCommand: IRequest<bool>
    {
        public Reservas UpdateReservas { get; set; }
    }
}
