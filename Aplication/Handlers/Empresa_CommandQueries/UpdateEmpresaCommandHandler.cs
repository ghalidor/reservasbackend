
using Aplication.CommandsQueries.Empresa_CommandQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Empresa_CommandQueries
{
    internal class UpdateEmpresaCommandHandler : IRequestHandler<UpdateEmpresaCommand, ServiceResponse>
    {
        private readonly IEmpresaRepository _empresaRepository;
        public UpdateEmpresaCommandHandler(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
        public async Task<ServiceResponse> Handle(UpdateEmpresaCommand query, CancellationToken cancellationToken)
        {
            var empresa = query.UpdateEmpresa;
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.response = await _empresaRepository.UpdateEmpresa(empresa);
                if (response.response)
                {
                    response.message = "Actualizado Corréctamente";
                }
            }
            catch (Exception ex)
            {
                response.message = "Error al Actualizar, " + ex.Message;
            }

            return response;
        }
    }
}
