using System.Net;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DiegoMoyanoProject.Repository
{
    public class EmailSender: IEmailSender
    {
        public async Task SendEmailInvertir(string mail, string nombre, int cantInvertir)
        {
            using (var client = new SmtpClient("smtp-mail.outlook.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("locorobinson@hotmail.com", "rickyrobinson1410");

            var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("locorobinson@hotmail.com");
                mailMessage.To.Add(new MailAddress(mail));
                mailMessage.To.Add(new MailAddress("Rickyrobinson1410@gmail.com"));
                mailMessage.Subject = "Mail Automatico invertir dinero";
                mailMessage.Body = $"NOMBRE: {nombre}\n  CANTIDAD A INVERTIR: {cantInvertir} \n";
                // Esperar 10 segundos antes de enviar el correo electrónico
                await Task.Delay(1000);

                // Envía el correo electrónico de manera asincrónica
                await client.SendMailAsync(mailMessage);
        
            }

        }

        public async Task SendEmailRetirar(string mail, string nombre, int cantRetirar)
        {
            using (var client = new SmtpClient("smtp-mail.outlook.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("locorobinson@hotmail.com", "rickyrobinson1410");

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("locorobinson@hotmail.com");
                mailMessage.To.Add(new MailAddress(mail));
                mailMessage.To.Add(new MailAddress("Rickyrobinson1410@gmail.com"));
                mailMessage.Subject = "Mail automatico retirar dinero";
                mailMessage.Body = $"NOMBRE: {nombre}\n  CANTIDAD A Retirar: {cantRetirar} \n";
                // Esperar 10 segundos antes de enviar el correo electrónico
                await Task.Delay(1000);

                // Envía el correo electrónico de manera asincrónica
                await client.SendMailAsync(mailMessage);

            }
        }
    }
}
