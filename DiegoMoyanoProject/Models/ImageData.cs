using System.ComponentModel;
using System.Reflection.Metadata;

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
        private DateTime _dateUploaded;
        private ImageFile? _sales;
        private ImageFile? _spentMoney;
        private ImageFile? _campaings;
        private ImageFile? _listings;
        private ImageFile? _totaCampaigns;

        public ImageData()
        {
        }

        public ImageData(DateTime dateUploaded)
        {
            _dateUploaded = dateUploaded;
        }

        public DateTime DateUploaded { get => _dateUploaded; set => _dateUploaded = value; }
        public ImageFile? Sales { get => _sales; set => _sales = value; }
        public ImageFile? SpentMoney { get => _spentMoney; set => _spentMoney = value; }
        public ImageFile? Campaings { get => _campaings; set => _campaings = value; }
        public ImageFile? Listings { get => _listings; set => _listings = value; }
        public ImageFile? TotaCampaigns { get => _totaCampaigns; set => _totaCampaigns = value; }
        static public List<ImageType> AllTypes = new List<ImageType> { ImageType.Sales, ImageType.SpentMoney, ImageType.Campaigns, ImageType.Listings, ImageType.TotalCampaigns };
    }
}