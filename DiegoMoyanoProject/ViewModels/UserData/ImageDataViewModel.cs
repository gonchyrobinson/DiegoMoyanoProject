using DiegoMoyanoProject.Models;
using System.ComponentModel;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class ImageDataViewModel
    {
        public ImageDataViewModel() { }
        public string? Blob64img { get; set; }
        public ImageType ImageType { get; set; }
        public string GetImageTypeDescription()
        {
            var field = ImageType.GetType().GetField(ImageType.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? ImageType.ToString() : attribute.Description;
        }

    }
}
