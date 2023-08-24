

using Aplication.CommandsQueries.Empresa_CommandQueries;
using Domain;
using MediatR;

namespace Aplication.Handlers.Empresa_CommandQueries
{
    public class GetUsuarioIdQueryHandler : IRequestHandler<GetUsuarioIdQuery, UsuarioResponse>
    {
       
        public GetUsuarioIdQueryHandler()
        {
            
        }
        public async Task<UsuarioResponse> Handle(GetUsuarioIdQuery query, CancellationToken cancellationToken)
        {
            UsuarioResponse response=new UsuarioResponse() ;

            if(query.Usuario_Id == 0)
            {
                response = new UsuarioResponse();
            }
            else
            {
                if(query.Usuario_Id == 1)
                {
                    response.UsuarioId = 1;
                    response.Usuario = "admin1020";
                    response.Password = "203040";
                }
                else
                {
                    response = new UsuarioResponse();
                }

            }
            return response;
        }
    }
}
