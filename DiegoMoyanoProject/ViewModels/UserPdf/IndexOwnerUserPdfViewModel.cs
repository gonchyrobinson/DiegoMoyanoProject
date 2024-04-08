using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class IndexOwnerUserPdfViewModel
    {
        //public List<DateTime> Dates { get; set; }
        public PdfData? PdfData { get; set; }
        public IndexOwnerUserPdfViewModel() { }
        
        public IndexOwnerUserPdfViewModel(PdfData? pdfData)
        {
            PdfData = pdfData;
        }
    }
}
