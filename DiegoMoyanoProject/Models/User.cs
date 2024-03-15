namespace DiegoMoyanoProject.Models
{
    public enum Role
    {
        Admin = 1,
        Operative = 2
    }
    public class User
    {
        private int id;
        private string mail;
        private string pass;
        private Role role;

        public int Id { get => id; set => id = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Pass { get => pass; set => pass = value; }
        public Role Role { get => role; set => role = value; }
    }
}
