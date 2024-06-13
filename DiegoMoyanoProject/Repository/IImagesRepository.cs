using DiegoMoyanoProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DiegoMoyanoProject.Repository
{
    public interface IImagesRepository
    {
        public bool AddDate(DateTime date);
        public bool Update(ImageFile img, DateTime date, int id);
        public bool Delete(DateTime date, ImageType type, int id);
        public bool DeleteRow(int id);
        public ImageFile? getImage(DateTime date, ImageType type, int id);
        public bool deleteOlderIfNeeded(int maxSupported = 3);
        public int countImagesAdded();
        public List<FileDate> GetAllDatesAndId();
        public int? GetMaxId();
    }
}
