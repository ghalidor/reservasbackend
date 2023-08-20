using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class UpdateReservasCommandHandler : IRequestHandler<UpdateReservasCommand, bool>
    {
        private readonly IReservasRepository _reservasRepository;
        public UpdateReservasCommandHandler(IReservasRepository reservasRepository)
        {
            _reservasRepository = reservasRepository;
        }

        public async Task<bool> Handle(UpdateReservasCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateReservas is null)
            {
                throw new ApplicationException("There is a problem in mapper");
            }
            return await _reservasRepository.UpdateReserva(request.UpdateReservas);

        }
    }
}
