using MediatR;

namespace Aplication.CommandsQueries.ReservasCommandQueries
{
    public class UpdateReservaEstadoCommand : IRequest<bool>
    {
        public int ReservaId { get; set; }
        public int Estado { get; set; }
        public string Motivo { get; set; }
    }
}
