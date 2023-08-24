
using Domain;
using MediatR;

namespace Aplication.CommandsQueries.Empresa_CommandQueries
{
    public class GetUsuarioIdQuery : IRequest<UsuarioResponse>
    {
        public int Usuario_Id { get; set; }
    }
}
