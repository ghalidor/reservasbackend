
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Mesas_CommandsQueries
{
    public class CreateMesasCommand : IRequest<ServiceResponse>
    {
        public Mesas CreateMesa { get; set; }
    }
}
