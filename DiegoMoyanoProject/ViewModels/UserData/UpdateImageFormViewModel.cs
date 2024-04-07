using DiegoMoyanoProject.Models;
using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class UpdateImageFormViewModel
    {

        [Required]
        [Display(Name="Tipo de Imagen")]
        public ImageType ImageType { get; set; }
        [Display(Name = "Imagen")]
        public IFormFile? InputFile { get; set; }
        public int Order { get; set; }
        public UpdateImageFormViewModel()
        {
        }

        public UpdateImageFormViewModel(ImageType imageType, int order)
        {
            ImageType = imageType;
            this.Order = order; 
        }
    }
}
