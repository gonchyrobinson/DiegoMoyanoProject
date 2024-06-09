using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class IndexOwnerUserPdfViewModel
    {
        //public List<DateTime> Dates { get; set; }
        public PdfData? PdfData { get; set; }
        public DateTime SelectedDate { get; set; }
        public List<DateTime> Dates { get; set; }
        public IndexOwnerUserPdfViewModel() { }

        public IndexOwnerUserPdfViewModel(PdfData? pdfData, DateTime selectedDate, List<DateTime> dates) : this(pdfData)
        {
            SelectedDate = selectedDate;
            Dates = dates;
        }

        public IndexOwnerUserPdfViewModel(PdfData? pdfData)
        {
            PdfData = pdfData;
        }
    }
}
