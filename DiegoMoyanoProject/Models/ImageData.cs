using System.ComponentModel;
using System.Reflection;

namespace DiegoMoyanoProject.Models
{
    public enum ImageType
    {
        [Description("Ventas")]
        Sales = 0,
        [Description("Dinero Invertido")]
        SpentMoney = 1,
        [Description("Campañas")]
        Campaigns = 2,
        [Description("Articulos de Venta")]
        Listings = 3
    }
    public static class Extensions
    {

        public static string GetDescription(this ImageType imageType)
        {
            var typeInfo = typeof(ImageType);
            FieldInfo field = typeInfo.GetField(imageType.ToString());
            object[] attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes != null && attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : imageType.ToString();
        }
    }

    


    public class ImageData
    {
        private int userId;
        private string path;
        private ImageType imageType;

        public ImageData()
        {
        
            }

        public ImageData(int userId, string path, ImageType imageType)
        {
            this.userId = userId;
            this.path = path;
            this.imageType = imageType;
        }

        public int UserId { get => userId; set => userId = value; }
        public string Path { get => path; set => path = value; }
        public ImageType ImageType { get => imageType; set => imageType = value; }
        public string GetImageTypeDescription()
        {
            var field = imageType.GetType().GetField(imageType.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? imageType.ToString() : attribute.Description;
        }

    }
}
