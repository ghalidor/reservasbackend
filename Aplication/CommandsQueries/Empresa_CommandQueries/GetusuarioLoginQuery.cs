
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Empresa_CommandQueries
{
    public class GetusuarioLoginQuery : IRequest<UsuarioResponse>
    {
        public UsuarioLogin usuario { get; set; }
    }
}
