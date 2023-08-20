
using Aplication.CommandsQueries.Mesas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Mesas_CommandsQueries
{
    public class ListMesasxZonaQueryHandler : IRequestHandler<ListMesasxZonaQuery, IEnumerable<Mesas>>
    {
        private readonly IMesasRepository _mesasRepository;
        public ListMesasxZonaQueryHandler(IMesasRepository mesasRepository)
        {
            _mesasRepository = mesasRepository;
        }
        public async Task<IEnumerable<Mesas>> Handle(ListMesasxZonaQuery query, CancellationToken cancellationToken)
        {
            var zona_id = query.zona_id;
            IEnumerable<Mesas> lista = new List<Mesas>();
            if (zona_id > 0)
            {
                lista = await _mesasRepository.ListMesas();
                lista = lista.Where(x => x.ZonaId == zona_id);
            }

            return lista;
        }
    }
}
