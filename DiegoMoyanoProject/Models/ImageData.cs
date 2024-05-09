using System.ComponentModel;

namespace DiegoMoyanoProject.Models
{
    public enum ImageType
    {
        [Description("Ventas")]
        Sales = 0,
        [Description("Total Ventas en el Año")]
        SpentMoney = 1,
        [Description("Gasto en Campañas Publicitarias")]
        Campaigns = 2,
        [Description("Productos Activos y en Venta")]
        Listings = 3,
        [Description("Gasto en Campañas Publicitarias en el Año")]
        TotalCampaigns = 4
    }
    public class ImageData
    {
        private string path;
        private ImageType imageType;
        private int order;

        public ImageData()
        {
        }

        public ImageData(string path, ImageType imageType, int order)
        {
            this.path = path;
            this.imageType = imageType;
            this.order = order;
        }

        public string Path { get => path; set => path = value; }
        public ImageType ImageType { get => imageType; set => imageType = value; }
        public int Order { get => order; set => order = value; }

        public string GetImageTypeDescription()
        {
            var field = imageType.GetType().GetField(imageType.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? imageType.ToString() : attribute.Description;
        }

    }
}