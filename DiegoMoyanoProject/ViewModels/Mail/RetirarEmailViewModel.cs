using System.ComponentModel.DataAnnotations;
using DiegoMoyanoProject.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
namespace DiegoMoyanoProject.ViewModels.Mail
{
    public class RetirarEmailViewModel
    {
        public RetirarEmailViewModel(string name, string email, int cantRetirar)
        {
            Name = name;
            Email = email;
            CantRetirar = cantRetirar;
        }
        public RetirarEmailViewModel()
        {
            Name = "";
            Email = "";
            CantRetirar = 0;
        }
        public RetirarEmailViewModel(string name, string mail)
        {
            Name = name;
            Email = mail;
            CantRetirar = 0;
        }


        [Display(Name = "Nombre de Usuario")]
        public string Name { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Display(Name = "Cantidad que desea retirar")]
        [Required(ErrorMessage = "Por favor ingrese una cantidad válida")]
        public int CantRetirar { get; set; }

    }
}
