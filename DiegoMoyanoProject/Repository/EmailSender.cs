using DiegoMoyanoProject.Models;
using System.Net;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DiegoMoyanoProject.Repository
{
    public class EmailSender: IEmailSender
    {
        private readonly string _email;
        private readonly string _pass;

        public EmailSender()
        {
            _email = "rickyrobinson1410@gmail.com";
            _pass = "jzchpaytawezqkch";
        }

        public async Task SendEmail(Email mail)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_email, _pass);

            var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_email);
                mailMessage.To.Add(new MailAddress(mail.Mail));
                mailMessage.To.Add(new MailAddress("Rickyrobinson1410@gmail.com"));
                mailMessage.Subject = mail.Subject;
                mailMessage.Body = mail.Body;
                // Esperar 10 segundos antes de enviar el correo electrónico
                await Task.Delay(1000);

                // Envía el correo electrónico de manera asincrónica
                await client.SendMailAsync(mailMessage);
        
            }

        }

    }
}
