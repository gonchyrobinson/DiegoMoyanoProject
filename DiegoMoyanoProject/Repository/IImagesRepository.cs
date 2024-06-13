using DiegoMoyanoProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DiegoMoyanoProject.Repository
{
    public interface IImagesRepository
    {
        public bool AddDate(DateTime date);
        public bool Update(ImageFile img,  int id);
        public bool Delete( ImageType type, int id);
        public bool DeleteRow(int id);
        public ImageFile? getImage(ImageType type, int id);
        public bool deleteOlderIfNeeded(int maxSupported = 3);
        public int countImagesAdded();
        public List<FileDate> GetAllDatesAndId();
        public int? GetMaxId();
    }
}
