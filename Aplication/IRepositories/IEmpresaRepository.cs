
using Domain;

namespace Aplication.IRepositories
{
    public interface IEmpresaRepository
    {
        Task<Empresa> RegistroEmpresa();
        Task<bool> UpdateEmpresa(Empresa reserva);
    }
}
