using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserDataRepository
    {
        public bool UploadImage(ImageData image);
        public bool UpdateImage(ImageData image, int order);
        public string? GetImagePath(ImageType type);
        public ImageData? GetImage(ImageType type, int order);
        public List<ImageData> GetUserImages();
        public List<ImageData> GetUserImages(DateTime date);
        public bool ExistsImage(ImageType type, int order);
        public bool DeleteImage(ImageType type, DateTime date);
        public int CountImages();
        public List<DateTime> GetAllDates();
        public int? MaxOrder(ImageType type);
        public bool AddOrder(ImageType type);
        public bool DeleteImage(ImageType type, int order);



    }
}