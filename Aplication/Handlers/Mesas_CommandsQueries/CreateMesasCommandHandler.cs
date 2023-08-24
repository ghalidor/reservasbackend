

using Aplication.CommandsQueries.Mesas_CommandsQueries;
using Aplication.IRepositories;
using Domain;
using MediatR;

namespace Aplication.Handlers.Mesas_CommandsQueries
{
    public class CreateMesasCommandHandler : IRequestHandler<CreateMesasCommand, ServiceResponse>
    {
        private readonly IMesasRepository _mesasRepository;
        public CreateMesasCommandHandler(IMesasRepository mesasRepository)
        {
            _mesasRepository = mesasRepository;
        }
        public async Task<ServiceResponse> Handle(CreateMesasCommand query, CancellationToken cancellationToken)
        {
            var empresa = query.CreateMesa;
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.response = await _mesasRepository.CreateMesas(empresa);
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
