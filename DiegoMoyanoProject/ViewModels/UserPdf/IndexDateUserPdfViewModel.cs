namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class IndexDateUserPdfViewModel
    {
        public byte[]? Pdf { get; set; }
        public List<DateTime> Dates { get; set; }

        public IndexDateUserPdfViewModel() { }

        public IndexDateUserPdfViewModel(byte[]? pdf, List<DateTime> dates)
        {
            Pdf = pdf;
            Dates = dates;
        }

    }
}
