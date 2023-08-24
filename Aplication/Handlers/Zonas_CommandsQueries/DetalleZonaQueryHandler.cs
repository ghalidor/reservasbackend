using Aplication.CommandsQueries.Zonas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Zonas_CommandsQueries
{
    public class DetalleZonaQueryHandler : IRequestHandler<DetalleZonaQuery, Zonas>
    {
        private readonly IZonasRepository _zonasRepository;
        public DetalleZonaQueryHandler(IZonasRepository zonasRepository)
        {
            _zonasRepository = zonasRepository;
        }
        public async Task<Zonas> Handle(DetalleZonaQuery query, CancellationToken cancellationToken)
        {
            var zona_id = query.ZonaId;
            return await _zonasRepository.ZonaDetalle(zona_id); ;
        }
    }
}
