
using Aplication.CommandsQueries.Zonas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Zonas_CommandsQueries
{
    public class CreateZonaCommandHandler : IRequestHandler<CreateZonaCommand, ServiceResponse>
    {
        private readonly IZonasRepository _zonaRepository;
        public CreateZonaCommandHandler(IZonasRepository zonaRepository)
        {
            _zonaRepository = zonaRepository;
        }
        public async Task<ServiceResponse> Handle(CreateZonaCommand query, CancellationToken cancellationToken)
        {
            var empresa = query.CreateZona;
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.response = await _zonaRepository.CreateZona(empresa);
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
