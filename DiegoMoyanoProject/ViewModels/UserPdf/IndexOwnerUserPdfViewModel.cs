using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class IndexOwnerUserPdfViewModel
    {
        //public List<DateTime> Dates { get; set; }
        public PdfData? PdfData { get; set; }
        public DateTime SelectedDate { get; set; }
        public List<FileDate> Dates { get; set; }
        public int SelectedId { get; set; }
        public IndexOwnerUserPdfViewModel() { }

        public IndexOwnerUserPdfViewModel(PdfData? pdfData, DateTime selectedDate, List<FileDate> dates, int id) : this(pdfData)
        {
            SelectedDate = selectedDate;
            Dates = dates;
            SelectedId = id;
        }

        public IndexOwnerUserPdfViewModel(PdfData? pdfData)
        {
            PdfData = pdfData;
        }
    }
}
