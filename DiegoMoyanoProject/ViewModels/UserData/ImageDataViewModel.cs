using DiegoMoyanoProject.Models;
using System.ComponentModel;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class ImageDataViewModel
    {

        public ImageDataViewModel() { }

        public ImageDataViewModel(string? path, ImageType imageType)
        {
            Path = path;
            ImageType = imageType;
        }

        public string? Path { get; set; }
        public ImageType ImageType { get; set; }
        public string GetImageTypeDescription()
        {
            var field = ImageType.GetType().GetField(ImageType.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? ImageType.ToString() : attribute.Description;
        }

    }
}
