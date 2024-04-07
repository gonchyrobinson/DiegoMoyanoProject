using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.User
{
    public class UserUploadDataViewModel
    {
        public UserUploadDataViewModel(int id, decimal capitalInvested, decimal rentability)
        {
            Id = id;
            CapitalInvested = capitalInvested;
            Rentability = rentability;
        }

        public UserUploadDataViewModel()
        {
        }
        public UserUploadDataViewModel(Models.User usu)
        {
            Id = usu.Id;
            CapitalInvested = usu.CapitalInvested;
            Rentability = usu.Rentability;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Capital Invertido")]
        public decimal CapitalInvested { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Rentabilidad")]
        public decimal Rentability { get; set; }
    }
}