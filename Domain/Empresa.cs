
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
    }
}
