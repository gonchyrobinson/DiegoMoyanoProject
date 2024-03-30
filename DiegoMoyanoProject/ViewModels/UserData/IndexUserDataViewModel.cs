namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class IndexUserDataViewModel
    {
        public int UserId { get; set; } 
        public List<ImageDataViewModel> Images { get; set; }
        public bool IsLoguedUser { get; set; }
        public bool IsAdminOrOwner { get; set; }
        public IndexUserDataViewModel() { }

        public IndexUserDataViewModel(int userId, List<ImageDataViewModel> images, bool isLoguedUser, bool isAdminOrOwner)
        {
            UserId = userId;
            Images = images;
            IsLoguedUser = isLoguedUser;
            IsAdminOrOwner = isAdminOrOwner;
        }
    }
}
