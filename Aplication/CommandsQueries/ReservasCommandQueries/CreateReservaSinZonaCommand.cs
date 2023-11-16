

using Domain;
using MediatR;

namespace Aplication.CommandsQueries.ReservasCommandQueries
{
    public class CreateReservaSinZonaCommand : IRequest<ServiceResponse>
    {
        public ReservacionNuevoSinZona? NewReservas { get; set; }
    }
}
