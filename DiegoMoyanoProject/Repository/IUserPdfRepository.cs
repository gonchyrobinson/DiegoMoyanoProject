using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserPdfRepository
    {
        public bool AddDate(DateTime date);
        public bool UpdatePdf(PdfData pdf, int id);
        public PdfData? GetPdfData( int id);
        public bool DeletePdf(int id);
        public List<FileDate> GetAllDates();
        public bool deleteOlderIfNeeded(int maxSupported = 3);
        public int countImagesAdded();
        public byte[]? GetPdf(int id);
        public int GetMaxId();
        public bool DeleteRow(int id);


    }
}