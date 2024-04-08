using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class UpdatePdfFormViewModel
    {
        [Display(Name = "Imagen")]
        public IFormFile? InputFile { get; set; }
        public int Order { get; set; }
        public UpdatePdfFormViewModel() { }
        public UpdatePdfFormViewModel(int order)
        {
            this.Order = order;
        }
    }
}
