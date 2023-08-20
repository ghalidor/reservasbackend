
using Aplication.CommandsQueries.Zonas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Zonas_CommandsQUeries
{
    public class ListZonasQueryHandler : IRequestHandler<ListZonasQuery, IEnumerable<Zonas>>
    {
        private readonly IZonasRepository _zonasRepository;
        public ListZonasQueryHandler(IZonasRepository zonasRepository)
        {
            _zonasRepository = zonasRepository;
        }
        public async Task<IEnumerable<Zonas>> Handle(ListZonasQuery query, CancellationToken cancellationToken)
        {
            return await _zonasRepository.ListZonas();
        }

    }
}
