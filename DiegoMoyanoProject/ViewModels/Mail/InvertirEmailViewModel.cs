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
            Email = email;
            CantInvertir = cantInvertir;
        }
        public InvertirEmailViewModel()
        {
            Name = "";
            Email = "";
            CantInvertir = 0;
        }
        public InvertirEmailViewModel(string name, string mail)
        {
            Name = name;
            Email = mail;
            CantInvertir = 0;
        }


        [Display(Name = "Nombre de Usuario")]
        public string Name { get; set; }
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Display(Name = "Cantidad a invertir")]
        [Required(ErrorMessage = "Por favor ingrese una cantidad válida")]
        public int CantInvertir { get; set; }
 
    }
}
