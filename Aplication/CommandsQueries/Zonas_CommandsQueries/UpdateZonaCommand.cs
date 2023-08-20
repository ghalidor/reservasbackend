
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Zonas_CommandsQueries
{
    public class UpdateZonaCommand : IRequest<ServiceResponse>
    {
        public Zonas UpdateZona { get; set; }

    }
}
