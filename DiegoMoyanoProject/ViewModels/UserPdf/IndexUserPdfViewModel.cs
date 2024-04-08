using DiegoMoyanoProject.ViewModels.UserData;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class IndexUserPdfViewModel
    {
            public string? Path { get; set; }
            public List<DateTime> Dates { get; set; }

            public IndexUserPdfViewModel() { }

            public IndexUserPdfViewModel(string? path, List<DateTime> dates)
            {
                Path = path;
                Dates = dates;
            }
    }
}
