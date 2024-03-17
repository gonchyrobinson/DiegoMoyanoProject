using DiegoMoyanoProject.Models;
using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.User
{
    public class CreateUserViewModel
    {
        //public CreateUserViewModel(string username, string pass, string mail)
        //{
        //    Username = username;
        //    Pass = pass;
        //    Mail = mail;
        //}
        public CreateUserViewModel()
        {
            Username = "";
            Pass = "";
            Mail = "";
            this.Role = Role.Operative; 
        }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Contraseña del Usuario")]

        public string Pass { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Correo Electronico")]
        [EmailAddress(ErrorMessage = "Debe ser un email")]
        public string Mail { get; set; }
        public Role Role { get; set; }  

    }
}
