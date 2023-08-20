
using Aplication.CommandsQueries.Zonas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Zonas_CommandsQueries
{
    public class ListZonasMesasAsignadasQueryHandler : IRequestHandler<ListZonasMesasAsignadasQuery, IEnumerable<ZonasMesasAsignadas>>
    {
        private readonly IZonasRepository _zonasRepository;
        public ListZonasMesasAsignadasQueryHandler(IZonasRepository zonasRepository)
        {
            _zonasRepository = zonasRepository;
        }
        public async Task<IEnumerable<ZonasMesasAsignadas>> Handle(ListZonasMesasAsignadasQuery query, CancellationToken cancellationToken)
        {
            return await _zonasRepository.ListMesasAsignadasZona();
        }
    }
}
