using DiegoMoyanoProject.Models;
using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.User
{
    public class UpdateUserViewModel
    {
        public UpdateUserViewModel(string username, Role role, string pass, int id, string mail)
        {
            Username = username;
            Role = role;
            Pass = pass;
            Id = id;
            Mail = mail;
        }
        public UpdateUserViewModel()
        {
            Username = "";
            Role = Role.Operative;
            Pass = "";
            Id = 0;
            Mail = "";
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Rol de Usuario")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Contraseña")]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Correo Electronico")]
        public string Mail { get; set; }
    }
}
