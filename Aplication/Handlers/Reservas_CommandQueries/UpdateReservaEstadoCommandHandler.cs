
using Aplication.CommandsQueries.ReservasCommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Reservas_CommandQueries
{
    public class UpdateReservaEstadoCommandHandler : IRequestHandler<UpdateReservaEstadoCommand, bool>
    {
        private readonly IReservasRepository _reservasRepository;
        public UpdateReservaEstadoCommandHandler(IReservasRepository reservasRepository)
        {
            _reservasRepository = reservasRepository;
        }

        public async Task<bool> Handle(UpdateReservaEstadoCommand request, CancellationToken cancellationToken)
        {
            if(request.ReservaId ==0)
            {
                throw new ApplicationException("There is a problem in mapper");
            }
            Reservas registro = new Reservas();
            registro.ReservaId = request.ReservaId;
            registro.Estado = request.Estado;
            registro.Motivo = request.Motivo;
            return await _reservasRepository.UpdateReservaEstado(registro);

        }
    }
}
