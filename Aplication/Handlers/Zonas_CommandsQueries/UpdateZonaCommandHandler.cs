
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
            var zona = query.UpdateZona;
            ServiceResponse response = new ServiceResponse();
            try
            {
                if(zona != null)
                {
                    if(zona.ZonaId != 0)
                    {
                        response.response = await _zonaRepository.UpdateZona(zona);
                        if(response.response)
                        {
                            response.message = "Registrado Corréctamente";
                        }
                    }
                    else
                    {
                        response.message = "Erro no se envio el ID";
                    }
                }
                else
                {
                    response.message = "No se envio data";
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
