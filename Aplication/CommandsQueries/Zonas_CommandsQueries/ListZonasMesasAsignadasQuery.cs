
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Zonas_CommandsQueries
{
    public class ListZonasMesasAsignadasQuery : IRequest<IEnumerable<ZonasMesasAsignadas>>
    {
    }
}
