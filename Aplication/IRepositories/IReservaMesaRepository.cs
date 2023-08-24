
using Domain;

namespace Aplication.IRepositories
{
    public interface IReservaMesaRepository
    {
        Task<IEnumerable<ReservaMesa>> ListaReservaMesa(int reservaId);
        Task<IEnumerable<ReservaMesa>> ListaReservaMesaDia(DateTime fecha);
        Task<IEnumerable<ReservaMesaCompleto>> ListaReservaMesaRango(DateTime fechaini, DateTime fechafin);
        Task<bool> CreateReservaMesa(ReservaMesa reserva);
    }
}
