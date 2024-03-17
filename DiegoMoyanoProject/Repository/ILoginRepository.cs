using DiegoMoyanoProject.Models;
namespace DiegoMoyanoProject.Repository
{
    public interface ILoginRepository
    {
        public User? Log(string mail, string pass);
    }
}
