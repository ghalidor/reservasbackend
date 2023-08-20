
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
}
