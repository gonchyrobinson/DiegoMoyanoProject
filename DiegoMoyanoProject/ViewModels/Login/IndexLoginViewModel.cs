using DiegoMoyanoProject.Models;
using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.Login
{
    public class IndexLoginViewModel
    {
        [Required (ErrorMessage = "Campo requerido")]
        [EmailAddress(ErrorMessage = "Debe ser un email")]
        [Display (Name = "Dirección de correo")]
        public string Mail { get; set; }
        [Required (ErrorMessage = "Campo requerido")]
        [Display (Name = "Contraseña")]
        public string Pass { get; set; }
    }
}
