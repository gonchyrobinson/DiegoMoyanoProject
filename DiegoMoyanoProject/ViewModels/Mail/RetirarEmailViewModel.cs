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
            Mail = email;
            Money = cantRetirar;
        }
        public RetirarEmailViewModel()
        {
            Name = "";
            Mail = "";
            Money = 0;
        }
        public RetirarEmailViewModel(string name, string mail)
        {
            Name = name;
            Mail = mail;
            Money = 0;
        }


        [Display(Name = "Nombre de Usuario")]
        public string Name { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Mail { get; set; }
        [Display(Name = "Cantidad que desea retirar")]
        [Required(ErrorMessage = "Por favor ingrese una cantidad válida")]
        public int Money { get; set; }

    }
}
