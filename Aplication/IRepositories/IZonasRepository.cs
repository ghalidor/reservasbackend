
using Domain;

namespace Aplication.IRepositories
{
    public interface IZonasRepository
    {
        Task<IEnumerable<Zonas>> ListZonas();
        Task<IEnumerable<ZonasMesasAsignadas>> ListMesasAsignadasZona();
        Task<bool> CreateZona(Zonas zona);
        Task<bool> UpdateZona(Zonas zona);
        Task<bool> DeleteZona(int ZonaId);
    }
}
