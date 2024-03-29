namespace DiegoMoyanoProject.Repository
{
    public interface IEmailSender
    {
       public  Task SendEmailInvertir(string mail, string nombre, int cantInvertir);
       public Task SendEmailRetirar(string mail, string nombre, int cantRetirar);
    }
}
