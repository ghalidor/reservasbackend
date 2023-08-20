
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Empresa_CommandQueries
{
    public class UpdateEmpresaCommand : IRequest<ServiceResponse>
    {
        public Empresa? UpdateEmpresa { get; set; }
    }
}
