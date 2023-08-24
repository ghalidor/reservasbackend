

using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Mesas_CommandsQueries
{
    public class DetalleMesaQuery : IRequest<Mesas>
    {
        public int MesaId { get; set; }
    }
}
