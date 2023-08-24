

using Aplication.CommandsQueries.Mesas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Mesas_CommandsQueries
{
    public class UpdateMesasCommandQuery : IRequestHandler<UpdateMesasCommand, ServiceResponse>
    {
        private readonly IMesasRepository _mesasRepository;
        public UpdateMesasCommandQuery(IMesasRepository mesasRepository)
        {
            _mesasRepository = mesasRepository;
        }
        public async Task<ServiceResponse> Handle(UpdateMesasCommand query, CancellationToken cancellationToken)
        {
            var empresa = query.UpdateMesa;
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.response = await _mesasRepository.UpdateMesas(empresa);
                if(response.response)
                {
                    response.message = "Registrado Corréctamente";
                }
            }
            catch(Exception ex)
            {
                response.message = "Error al Registrar, " + ex.Message;
            }

            return response;
        }
    }
}
