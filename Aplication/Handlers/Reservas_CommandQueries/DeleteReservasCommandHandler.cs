
using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using MediatR;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class DeleteReservasCommandHandler : IRequestHandler<DeleteReservasCommand, bool>
    {
        private readonly IReservasRepository _reservasRepository;
        public DeleteReservasCommandHandler(IReservasRepository reservasRepository)
        {
            _reservasRepository = reservasRepository;
        }

        public async Task<bool> Handle(DeleteReservasCommand request, CancellationToken cancellationToken)
        {
            return await _reservasRepository.DeleteReserva(request.Reserva_id);
        }

    }
}
