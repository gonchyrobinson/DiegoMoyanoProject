namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class IndexUserDataViewModel
    {
        public int UserId { get; set; } 
        public List<ImageDataViewModel> Images { get; set; }
        public IndexUserDataViewModel() { }

        public IndexUserDataViewModel(int userId, List<ImageDataViewModel> images)
        {
            this.UserId = userId;
            this.Images = images;
        }
    }
}
