using DiegoMoyanoProject.Models;
using System.ComponentModel;

namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class IndexOwnerUserDataViewModel
    {
        public List<ImageDataViewModel> Images { get; set; }
        public DateTime SelectedDate { get; set; }
        public List<FileDate> Dates { get; set; }
        public int SelectedId { get; set; }
        public IndexOwnerUserDataViewModel() { }
        public IndexOwnerUserDataViewModel(List<ImageDataViewModel> images, List<FileDate> dates, DateTime date, int id)
        {
            Images = images;
            Dates = dates;
            SelectedDate = date;
            SelectedId = id;
        }
    }
}
