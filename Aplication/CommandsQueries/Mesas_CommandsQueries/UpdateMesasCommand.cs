

using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Mesas_CommandsQueries
{
    public class UpdateMesasCommand : IRequest<ServiceResponse>
    {
        public Mesas UpdateMesa { get; set; }
    }
}
