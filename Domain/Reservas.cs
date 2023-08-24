
namespace Domain
{
    public class Reservas
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
        public string Correo { get; set; }
        public int Estado { get; set; }
        public string Motivo { get; set; }
    }

    public class ReservaEstado
    {
        public int ReservaId { get; set; }
        public int Estado { get; set; }
        public string Motivo { get; set; }
    }
    public class ReservacionNuevo
    {
        public int Personas { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int ZonaId { get; set; }
        public string NroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public bool Mascotas { get; set; }
        public string Correo { get; set; }
    }

    public class ReservacionHoras
    {
        public string Hora { get; set; }
        public DateTime Fecha { get; set; }
        public bool IsActivo { get; set; }

        public List<Zonas> ZonasLibres { get; set; }
    }

    public class ReservacionLista
    {
        public int ReservaId { get; set; }
        public string Zona { get; set; }
        public int ZonaId { get; set; }
        public string NroMesa { get; set; }
        public string Mesa { get; set; }
        public string MesaId { get; set; }
        public int Personas { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public bool Mascotas { get; set; }
        public string MascotasString { get; set; }
        public string Mensaje { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string ClaseEstado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaDatetime { get; set; }
        public string Correo { get; set; }
        public string Motivo { get; set; }
    }
}
