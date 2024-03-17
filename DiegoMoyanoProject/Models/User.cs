namespace DiegoMoyanoProject.Models
{
    public enum Role
    {
        Admin = 1,
        Owner = 2,
        Operative = 3
    }
    public class User
    {
        private int id;
        private string mail;
        private string pass;
        private string username;
        private Role role;

        public User()
        {
            id = 1;
            mail= string.Empty;
            pass = string.Empty;
            username = string.Empty;
            role = Role.Admin;
        }

        public User(int id, string mail, string pass, string username, Role role)
        {
            this.id = id;
            this.mail = mail;
            this.pass = pass;
            this.Username = username;
            this.role = role;
        }

        public int Id { get => id; set => id = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Pass { get => pass; set => pass = value; }
        public Role Role { get => role; set => role = value; }
        public string Username { get => username; set => username = value; }
    }
}
