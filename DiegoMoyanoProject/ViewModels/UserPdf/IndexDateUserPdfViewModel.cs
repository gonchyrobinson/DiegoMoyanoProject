namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class IndexDateUserPdfViewModel
    {
        public string? Path { get; set; }
        public List<DateTime> Dates { get; set; }

        public IndexDateUserPdfViewModel() { }

        public IndexDateUserPdfViewModel(string? path, List<DateTime> dates)
        {
            Path = path;
            Dates = dates;
        }

    }
}
