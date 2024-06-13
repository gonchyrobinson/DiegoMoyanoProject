using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class UpdatePdfFormViewModel
    {
        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public IFormFile? InputFile { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public UpdatePdfFormViewModel() { }
        public UpdatePdfFormViewModel(DateTime date, int id)
        {
            this.Date = date;
            this.Id =id ;
        }
    }
}
