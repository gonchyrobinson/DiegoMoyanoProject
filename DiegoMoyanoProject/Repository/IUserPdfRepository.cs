using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserPdfRepository
    {
        public bool AddDate(DateTime date);
        public bool UpdatePdf(PdfData pdf, DateTime date);
        public PdfData? GetPdfData( DateTime date);
        public bool DeletePdf(DateTime date);
        public List<DateTime> GetAllDates();
        public bool deleteOlderIfNeeded(int maxSupported = 3);
        public int countImagesAdded();
        public byte[]? GetPdf(DateTime date);

    }
}