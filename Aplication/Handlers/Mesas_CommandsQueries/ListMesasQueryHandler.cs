

using Aplication.CommandsQueries.Mesas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Mesas_CommandsQueries
{
    public class ListMesasQueryHandler : IRequestHandler<ListMesasQuery, IEnumerable<Mesas>>
    {
        private readonly IMesasRepository _mesasRepository;
        public ListMesasQueryHandler(IMesasRepository mesasRepository)
        {
            _mesasRepository = mesasRepository;
        }
        public async Task<IEnumerable<Mesas>> Handle(ListMesasQuery query, CancellationToken cancellationToken)
        {
            return await _mesasRepository.ListMesas();
        }
    }
}
