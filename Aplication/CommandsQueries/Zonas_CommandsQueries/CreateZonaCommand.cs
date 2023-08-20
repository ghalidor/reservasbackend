
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Zonas_CommandsQueries
{
    public class CreateZonaCommand : IRequest<ServiceResponse>
    {
        public Zonas CreateZona { get; set; }

    }
}
