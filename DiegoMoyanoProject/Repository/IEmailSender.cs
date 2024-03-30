using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IEmailSender
    {
       public  Task SendEmail(Email mail);
     
    }
}
