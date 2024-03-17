using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserRepository
    {
        public List<User> ListUsers();
        public User? GetUserById(int id);
        public bool CreateUser(User usu);
        public bool UpdateUser(int id, User usu);
        public bool DeleteUser(int id);
        public string? GetMail(int? id);
    }
    public interface IUserDataRepository
    {
        public bool UploadImg(byte[]? img);
        public byte[] DownloadImg();
    }
}