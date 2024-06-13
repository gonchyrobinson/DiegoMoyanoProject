namespace DiegoMoyanoProject.Models
{
    public class ImageFile
    {
        private string? path;
        private ImageType _ImageType;
        private int id;
        public ImageFile(string? blob64img, ImageType type)
        {
            this.path = blob64img;
            _ImageType = type;
        }
        public ImageFile(ImageType type)
        {
            this.path = null;
            _ImageType = type;
        }
        public ImageFile(ImageType type, int id)
        {
            this.path = null;
            _ImageType = type;
            this.id = id;
        }

        public ImageFile()
        {
        }
        public ImageFile(string type, string blob64string, ImageType t)
        {
            this.path = "data:image/"+type+";base64,"+blob64string;
            this._ImageType = t;
        }

        public ImageFile(string? path, ImageType imageType, int id) : this(path, imageType)
        {
            this.id = id;
        }

        public string? Path { get => path; set => path = value; }
        public ImageType ImageType { get => _ImageType; set => _ImageType = value; }
        public int Id { get => id; set => id = value; }
    }
}