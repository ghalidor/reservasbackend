using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Mesas_CommandsQueries
{
    public class ListMesasQuery : IRequest<IEnumerable<Mesas>>
    {
    }
}
