using System.ComponentModel.DataAnnotations;
using DiegoMoyanoProject.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
namespace DiegoMoyanoProject.ViewModels.Mail
{
    public class InvertirEmailViewModel
    {
        public InvertirEmailViewModel(string name, string email, int cantInvertir)
        {
            Name = name;
            Mail = email;
            Money = cantInvertir;
        }
        public InvertirEmailViewModel()
        {
            Name = "";
            Mail = "";
            Money = 0;
            CapitalInvested = 0;
        }
        public InvertirEmailViewModel(string name, string mail, decimal capitalInvested)
        {
            Name = name;
            Mail = mail;
            Money = 0;
            CapitalInvested = capitalInvested;
        }


        [Display(Name = "Nombre de Usuario")]
        public string Name { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Mail { get; set; }
        [Display(Name = "Cantidad a invertir")]
        [Required(ErrorMessage = "Por favor ingrese una cantidad válida")]
        public int Money { get; set; }
        public decimal CapitalInvested { get; set; }

    }
}
