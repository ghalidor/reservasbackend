using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Mesas_CommandsQueries
{
    public class ListMesasxZonaQuery : IRequest<IEnumerable<Mesas>>
    {
        public int zona_id { get; set; }
    }
}
