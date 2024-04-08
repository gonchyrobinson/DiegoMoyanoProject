using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserPdfRepository
    {
        public bool UploadPdf(PdfData pdf);
        public bool UpdatePdf(PdfData pdf, int order);
        public string? PdfPath(int order);
        public string? PdfPath(DateTime date);
        public PdfData? GetPdf( int order);
        public bool DeletePdf(DateTime date);
        public List<DateTime> GetAllDates();
        public bool AddOrder();
        public bool ReduceOrder();
        public bool DeletePdf(int order);
    }
}