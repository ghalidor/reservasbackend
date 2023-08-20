

using Domain;

namespace Aplication.IRepositories
{
    public interface IReservasRepository
    {
        Task<IEnumerable<Reservas>> ListaReservacion(DateTime fechaini, DateTime fechafin);
        Task<IEnumerable<Reservas>> ListaReservacionxDia(DateTime dia);
        Task<bool> CreateReserva(Reservas reserva);
        Task<int> CreateReservaReturnId(Reservas reserva);
        Task<bool> UpdateReserva(Reservas reserva);
        Task<bool> DeleteReserva(int reservaId);
    }
}
