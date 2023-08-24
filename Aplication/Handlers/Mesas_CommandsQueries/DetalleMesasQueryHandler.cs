

using Aplication.CommandsQueries.Mesas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Mesas_CommandsQueries
{
    public class DetalleMesasQueryHandler : IRequestHandler<DetalleMesaQuery, Mesas>
    {
        private readonly IMesasRepository _mesasRepository;
        public DetalleMesasQueryHandler(IMesasRepository mesasRepository)
        {
            _mesasRepository = mesasRepository;
        }
        public async Task<Mesas> Handle(DetalleMesaQuery query, CancellationToken cancellationToken)
        {
            var zona_id = query.MesaId;
            return await _mesasRepository.MesaDetalle(zona_id); ;
        }
    }
}
