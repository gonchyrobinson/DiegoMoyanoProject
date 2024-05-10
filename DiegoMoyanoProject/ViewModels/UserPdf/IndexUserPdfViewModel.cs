using DiegoMoyanoProject.ViewModels.UserData;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class IndexUserPdfViewModel
    {
            public byte[]? Pdf { get; set; }
            public List<DateTime> Dates { get; set; }

            public IndexUserPdfViewModel() { }

            public IndexUserPdfViewModel(byte[]? pdf, List<DateTime> dates)
            {
                Pdf = pdf;
                Dates = dates;
            }
    }
}
