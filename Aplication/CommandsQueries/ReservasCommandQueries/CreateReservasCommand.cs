
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.ReservasCommandQueries
{
    public class CreateReservasCommand : IRequest<ServiceResponse>
    {
        public ReservacionNuevo? NewReservas { get; set; }
    }
}
