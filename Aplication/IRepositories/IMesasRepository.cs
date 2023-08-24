
using Domain;

namespace Aplication.IRepositories
{
    public interface IMesasRepository
    {
        Task<IEnumerable<Mesas>> ListMesas();
        Task<IEnumerable<Mesas>> ListMesasLibres();
        Task<Mesas> MesaDetalle(int mesaid);
        Task<bool> CreateMesas(Mesas mesas);
        Task<bool> UpdateMesas(Mesas mesas);
        Task<bool> DeleteMesa(int mesaId);
    }
}
