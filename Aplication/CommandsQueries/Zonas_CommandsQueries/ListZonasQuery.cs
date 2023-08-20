using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Zonas_CommandsQueries
{
    public class ListZonasQuery : IRequest<IEnumerable<Zonas>>
    {
    }
}
