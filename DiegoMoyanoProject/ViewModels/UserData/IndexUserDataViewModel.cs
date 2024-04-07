namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class IndexUserDataViewModel
    { 
        public List<ImageDataViewModel> Images { get; set; }
        public List<DateTime> Dates { get; set; }   

        public IndexUserDataViewModel() { }

        public IndexUserDataViewModel(List<ImageDataViewModel> images, List<DateTime> dates)
        {
            Images = images;
            Dates = dates;
        }
    }
}
