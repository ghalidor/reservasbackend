
using Aplication.CommandsQueries.Empresa_CommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Empresa_CommandQueries
{
    public class RegistroEmpresaQueryHandler : IRequestHandler<RegistroEmpresaQuery, Empresa>
    {
        private readonly IEmpresaRepository _empresaRepository;
        public RegistroEmpresaQueryHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
        public async Task<Empresa> Handle(RegistroEmpresaQuery query, CancellationToken cancellationToken)
        {
           
            return await _empresaRepository.RegistroEmpresa();
        }
    }
}
