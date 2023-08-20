namespace Domain
{
    public class Zonas
    {
        public int ZonaId { get; set; }
        public string Descripcion { get; set; }
        public int SucursalId { get; set; }
        public bool EsActivo { get; set; }
        public int EmpresaId { get; set; }
        public bool Servidor { get; set; }
    }

    public class ZonasMesasAsignadas
    {
        public int ZonaId { get; set; }
        public string Descripcion { get; set; }
        public int Asignadas { get; set; }
        public int ParaReserva { get; set; }
    }
}