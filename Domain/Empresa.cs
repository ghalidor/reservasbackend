
namespace Domain
{
    public class Empresa
    {
        public int Empresa_id { get; set; }
        public string Nombre { get; set; }
        public int AtencionDiaInicio { get; set; }
        public int AtencionDiaFin { get; set; }
        public string AtencionHoraInicio { get; set; }
        public string AtencionHoraFin { get; set; }
        public string Telefono { get; set; }
        public int Personas { get; set; }
        public string direccion { get; set; }
    }

    public class UsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }

    }

    public class UsuarioLogin
    {
        public string Usuario { get; set; }
        public string Password { get; set; }

    }
    public class CorreoArchivoAdjunto
    {
        public MemoryStream? archivo { get; set; }
        public string? nombre { get; set; }
    }
}
