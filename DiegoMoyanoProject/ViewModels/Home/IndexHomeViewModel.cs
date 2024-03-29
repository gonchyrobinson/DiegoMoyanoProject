namespace DiegoMoyanoProject.ViewModels.Home
{
    public class IndexHomeViewModel
    {
        public bool IsLogued { get; set; }

        public IndexHomeViewModel(bool isLogued)
        {
            IsLogued = isLogued;
        }

        public IndexHomeViewModel() { }
    }
}
