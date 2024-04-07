using DiegoMoyanoProject.Models;
using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.User
{
    public class UserViewDataViewModel
    {
        public UserViewDataViewModel(string username, Role role, int id, string mail, bool isUser)
        {
            Username = username;
            Role = role;
            Id = id;
            Mail = mail;
            IsUser = isUser;
        }
        public UserViewDataViewModel()
        {
            Username = "";
            Role = Role.Operative;
            Id = 0;
            Mail = "";
            IsUser = false;

        }

        public UserViewDataViewModel(Models.User usu, bool IsOperativeUser)
        {
            Username = usu.Username;
            Role = usu.Role;
            Id = usu.Id;
            Mail = usu.Mail;
            IsUser = IsOperativeUser;
        }

        public int Id { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string Username { get; set; }

        [Display(Name = "Rol de Usuario")]
        public Role Role { get; set; }

        [Display(Name = "Correo Electronico")]
        public string Mail { get; set; }
        public bool IsUser { get; set; }
    }
}