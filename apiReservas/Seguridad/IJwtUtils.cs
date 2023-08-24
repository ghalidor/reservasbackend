using Domain;

namespace apiReservas.Seguridad
{
    public interface IJwtUtils
    {
        public string GenerateToken(UsuarioResponse user);
        public int? ValidateToken(string token);
    }
}
