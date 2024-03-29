using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserRepository
    {
        public List<User> ListUsers();
        public List<User> ListOperativeUsers();
        public User? GetUserById(int id);
        public bool CreateUser(User usu);
        public bool UpdateUser(int id, User usu);
        public bool DeleteUser(int id);
        public string? GetMail(int? id);
    }
}