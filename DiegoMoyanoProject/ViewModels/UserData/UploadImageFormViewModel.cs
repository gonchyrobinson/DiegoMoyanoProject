using DiegoMoyanoProject.Models;
using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class UploadImageFormViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public int UserId { get; set; }
        [Required]
        [Display(Name="Tipo de Imagen")]
        public ImageType ImageType { get; set; }
        [Display(Name = "Imagen")]
        public IFormFile? InputFile { get; set; }
        public UploadImageFormViewModel(int userId)
        {
            UserId = userId;
        }

        public UploadImageFormViewModel()
        {
        }

        public UploadImageFormViewModel(int userId, ImageType imageType) : this(userId)
        {
            ImageType = imageType;
        }
    }
}
