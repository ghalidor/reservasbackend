

using Domain;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Utilitarios
{
    
    public class CorreoSends
    {
        private readonly IConfiguration _configuracion;
        public CorreoSends(IConfiguration configuracion)
        {
           
            _configuracion = configuracion;
        }
        private void SenEmail(string destinatario, string asunto, string mensaje, bool esHtlm = false, List<CorreoArchivoAdjunto> adjuntos = null)
        {

            try
            {
                SmtpClient cliente;
                MailMessage email;
                string _USER;
                string _PASWWORD;
                string _HOST = "smtp.gmail.com";
                string _PORT = "587";
                string _ENABLESSL = "true";
                _USER = _configuracion["correo:usuario"];
                _PASWWORD = _configuracion["correo:password"];
                cliente = new SmtpClient(_HOST, Int32.Parse(_PORT))
                {
                    EnableSsl = Boolean.Parse(_ENABLESSL),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_USER, _PASWWORD)
                };
                email = new MailMessage(_USER, destinatario, asunto, mensaje);
                email.IsBodyHtml = esHtlm;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.SubjectEncoding = System.Text.Encoding.Default;
                if(adjuntos != null)
                {
                    foreach(var adjunto in adjuntos)
                    {
                        Attachment attachment = new Attachment(adjunto.archivo, adjunto.nombre, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.FileName = adjunto.nombre;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        email.Attachments.Add(attachment);
                    }
                }
                cliente.Send(email);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void envio_correoRemoto(Reservas registro,Empresa empresa, string destintariosRegistros)
        {
            string mensajeEnviar = $@"
                                    <div style='background-color:#F6F0EF;padding:5px;text-align:center'>
                                        <h3>Hola : <strong>{registro.Nombre}</strong> </h3>
                                        <h4>TU RESERVA ESTA CONFIRMADA</h4><br>
                                        <div>{empresa.Nombre}</div>
                                          <div>{empresa.direccion}</div>
                                        <div>{empresa.Telefono}</div><br>
 <div style=padding:5px;text-align:left'>A nombre de : <strong>{registro.Nombre}</strong></div>
 <div style=padding:5px;text-align:left'>  <strong>{registro.Personas}</strong> Personas</div>
 <div style=padding:5px;text-align:left'> <strong>{registro.Fecha.Date} {registro.Hora}</strong></div>
 <div style=padding:5px;text-align:left'>Código de reserva : <strong>{registro.ReservaId}</strong></div>
 <div style=padding:5px;text-align:left'>Mensaje : <strong>{registro.Mensaje}</strong></div><br>
                                      
                        <br><br>
                                    </div>";



            //string listaCorreosEnviar = String.Join(",", destintariosRegistros.Select(x => x.WEB_DestCorreo));
            SenEmail(destintariosRegistros, "RRESERVA REALIZADA DIA " + registro.Fecha, mensajeEnviar, true);
        }
    }
}
