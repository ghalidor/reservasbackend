
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Zonas_CommandsQueries
{
    public class DeleteZonaCommand : IRequest<ServiceResponse>
    {
        public int ZonaId { get; set; }
    }
}
