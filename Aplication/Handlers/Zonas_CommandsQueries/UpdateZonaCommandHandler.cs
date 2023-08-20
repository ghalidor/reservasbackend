
using Aplication.CommandsQueries.Zonas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Zonas_CommandsQueries
{
    public class UpdateZonaCommandHandler : IRequestHandler<UpdateZonaCommand, ServiceResponse>
    {
        private readonly IZonasRepository _zonaRepository;
        public UpdateZonaCommandHandler(IZonasRepository zonaRepository)
        {
            _zonaRepository = zonaRepository;
        }
        public async Task<ServiceResponse> Handle(UpdateZonaCommand query, CancellationToken cancellationToken)
        {
            var empresa = query.UpdateZona;
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.response = await _zonaRepository.UpdateZona(empresa);
                if (response.response)
                {
                    response.message = "Registrado Corréctamente";
                }
            }
            catch (Exception ex)
            {
                response.message = "Error al Registrar, " + ex.Message;
            }

            return response;
        }
    }
}
