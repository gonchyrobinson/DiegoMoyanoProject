using DiegoMoyanoProject.Models;
using System.ComponentModel;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class IndexOwnerUserDataViewModel
    {
        public List<ImageDataViewModel> Images { get; set; }
        public DateTime SelectedDate { get; set; }
        public List<DateTime> Dates { get; set; }
        public IndexOwnerUserDataViewModel() { }
        public IndexOwnerUserDataViewModel(List<ImageDataViewModel> images, List<DateTime> dates, DateTime date)
        {
            Images = images;
            Dates = dates;
            SelectedDate = date;
        }
    }
}
