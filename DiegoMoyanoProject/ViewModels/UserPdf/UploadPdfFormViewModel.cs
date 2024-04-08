using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class UploadPdfFormViewModel
    {
        [Display(Name = "Reporte")]
        public IFormFile? InputFile { get; set; }
        public UploadPdfFormViewModel()
        {

        }
    }
}
