using DiegoMoyanoProject.Models;
using System.ComponentModel.DataAnnotations;
using DiegoMoyanoProject.ViewModels;

namespace DiegoMoyanoProject.ViewModels.User
{
    public class UserOfIndexUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string Username { get; set; }

        [Display(Name = "Rol De Usuario")]
        public Role Role { get; set; }
        public string Mail { get; set; }
        public bool IsAdminOrUser { get; set; }

        public UserOfIndexUserViewModel()
        {
            Id = 0;
            Username = "";
            Role = Role.Operative;
            IsAdminOrUser = false;
            Mail = "";
        }

        public UserOfIndexUserViewModel(DiegoMoyanoProject.Models.User usu, bool IsAdminOrUser)
        {
            Id = usu.Id;
            Username = usu.Username;
            Role = usu.Role;
            this.IsAdminOrUser = IsAdminOrUser;
            this.Mail = usu.Mail;
        }
    }
}
