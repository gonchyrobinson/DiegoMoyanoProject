using DiegoMoyanoProject.Models;
using System.ComponentModel;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class IndexOwnerUserDataViewModel
    { 
        public List<DateTime> Dates {  get; set; }
        public ImageData? Sales {  get; set; }
        public ImageData? SpentMoney { get; set; }  
        public ImageData? Campaigns { get; set; }
        public ImageData? Listings { get; set; }
        public ImageData? TotalCampaigns { get; set; }
        public IndexOwnerUserDataViewModel() { }
        public string GetImageTypeDescription(ImageType img)
        {
            var field = img.GetType().GetField(img.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? img.ToString() : attribute.Description;
        }

    }
}
