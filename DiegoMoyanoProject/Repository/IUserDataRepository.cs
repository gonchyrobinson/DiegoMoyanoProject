using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Repository
{
    public interface IUserDataRepository
    {
        public bool UploadImage(ImageData image);
        public string? GetImagePath(int userId, ImageType type);
        public ImageData? GetImage(int userId, ImageType type);
        public List<ImageData> GetUserImages(int userId);
        public bool ExistsImage(int userId, ImageType type);
        public bool DeleteImage(int userId, ImageType type);
       
    }
}