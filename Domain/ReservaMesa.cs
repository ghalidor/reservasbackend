
namespace Domain
{
    public class ReservaMesa
    {
        public int ReservaMesaId { get; set; }
        public int ReservaId { get; set; }
        public int MesaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int Personas { get; set; }
        public int ZonaId { get; set; }
        
    }

    public class ReservaMesaCompleto
    {
        public int ReservaId { get; set; }
        public int Personas { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int MesaId { get; set; }
        public int ZonaId { get; set; }
        public string NroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public bool Mascotas { get; set; }
        public int Estado { get; set; }
        public int ReservaMesaId { get; set; }
        public int PersonasMesa { get; set; }
        public string Correo { get; set; }
        public string Motivo { get; set; }

    }
}
