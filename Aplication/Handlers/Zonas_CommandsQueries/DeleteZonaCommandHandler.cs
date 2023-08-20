
using Aplication.CommandsQueries.Zonas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Zonas_CommandsQueries
{
    public class DeleteZonaCommandHandler : IRequestHandler<DeleteZonaCommand, ServiceResponse>
    {
        private readonly IZonasRepository _zonaRepository;
        public DeleteZonaCommandHandler(IZonasRepository zonaRepository)
        {
            _zonaRepository = zonaRepository;
        }
        public async Task<ServiceResponse> Handle(DeleteZonaCommand query, CancellationToken cancellationToken)
        {
            var empresa = query.ZonaId;
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.response = await _zonaRepository.DeleteZona(empresa);
                if (response.response)
                {
                    response.message = "Eliminado Corréctamente";
                }
            }
            catch (Exception ex)
            {
                response.message = "Error al Eliminar, " + ex.Message;
            }

            return response;
        }
    }
}
