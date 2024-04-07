using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class IndexOwnerUserDataViewModel
    { 
        public List<DateTime> Dates {  get; set; }
        public ImageData? Sales {  get; set; }
        public ImageData? SpentMoney { get; set; }  
        public ImageData? Campaigns { get; set; }
        public ImageData? Listings { get; set; }
        public IndexOwnerUserDataViewModel() { }

    }
}
