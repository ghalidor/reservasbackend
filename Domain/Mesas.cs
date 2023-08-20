namespace Domain
{
    public class Mesas
    {
        public int MesaId { get; set; }
        public string Descripcion { get; set; }
        public int NumeroMesa { get; set; }
        public bool EsActiva { get; set; }
        public bool Ocupada { get; set; }
        public int SucursalId { get; set; }
        public int ZonaId { get; set; }
        public int EmpresaId { get; set; }
        public bool Servidor { get; set; }
        public string Tipo { get; set; }
        public int Pax { get; set; }
        public bool ParaReservar { get; set; }
    }
}
