


using Aplication.CommandsQueries.Empresa_CommandQueries;
using Domain;
using MediatR;

namespace Aplication.Handlers.Empresa_CommandQueries
{
    public class GetusuarioLoginQueryHandler : IRequestHandler<GetusuarioLoginQuery, UsuarioResponse>
    {

        public GetusuarioLoginQueryHandler()
        {

        }
        public async Task<UsuarioResponse> Handle(GetusuarioLoginQuery query, CancellationToken cancellationToken)
        {
            UsuarioResponse response = new UsuarioResponse();

            if(query.usuario ==null)
            {
                response = new UsuarioResponse();
            }
            else
            {
                if(query.usuario.Usuario == "admin1020" && query.usuario.Password == "203040")
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
