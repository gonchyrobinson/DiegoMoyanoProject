namespace DiegoMoyanoProject.Models
{
    public class UserData
    {
        private int userId;
        private ImageData? sales;
        private ImageData? spentMoney;
        private ImageData? campaigns;
        private ImageData? listings;

        public int UserId { get => userId; set => userId = value; }
        public ImageData? Sales { get => sales; set => sales = value; }
        public ImageData? SpentMoney { get => spentMoney; set => spentMoney = value; }
        public ImageData? Campaigns { get => campaigns; set => campaigns = value; }
        public ImageData? Listings { get => listings; set => listings = value; }
    }
}
