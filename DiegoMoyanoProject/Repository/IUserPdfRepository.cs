using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserPdfRepository
    {
        public bool UploadPdf(PdfData pdf);
        public bool UpdatePdf(PdfData pdf, int order);
        public byte[]? GetPdf(int order);
        public byte[]? GetPdf(DateTime date);
        public PdfData? GetPdfData( int order);
        public bool DeletePdf(DateTime date);
        public List<DateTime> GetAllDates();
        public bool AddOrder();
        public bool ReduceOrder();
        public bool DeletePdf(int order);
        public bool DeletePdfWithOrderMoreThan3(int order=3);
    }
}