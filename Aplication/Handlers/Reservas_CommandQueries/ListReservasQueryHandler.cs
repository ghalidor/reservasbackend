
using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class ListReservasQueryHandler : IRequestHandler<ListReservasQuery, IEnumerable<Reservas>>
    {
        private readonly IReservasRepository _reservasRepository;
        public ListReservasQueryHandler(IReservasRepository reservasRepository)
        {
            _reservasRepository = reservasRepository;
        }
        public async Task<IEnumerable<Reservas>> Handle(ListReservasQuery query, CancellationToken cancellationToken)
        {
            return await _reservasRepository.ListaReservacion(Convert.ToDateTime(query.fechaini), Convert.ToDateTime(query.fechafin));
        }
    }
}
