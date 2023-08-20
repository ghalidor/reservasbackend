

namespace Domain
{
    public class ServiceResponse
    {
        public bool response { get; set; }
        public string message { get; set; }
    }

    public class ServiceResponseReserva
    {
        public bool response { get; set; }
        public string message { get; set; }
        public List<ReservacionHoras> lista { get; set; }
    }
}