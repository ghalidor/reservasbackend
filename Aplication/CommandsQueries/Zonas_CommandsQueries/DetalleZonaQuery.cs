
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Zonas_CommandsQueries
{
    public class DetalleZonaQuery : IRequest<Zonas>
    {
        public int ZonaId { get; set; }
    }
}
