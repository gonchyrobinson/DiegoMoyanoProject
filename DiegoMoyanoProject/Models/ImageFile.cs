namespace DiegoMoyanoProject.Models
{
    public class ImageFile
    {
        private string? blob64img;
        private ImageType _ImageType;

        public ImageFile(string? blob64img, ImageType type)
        {
            this.blob64img = blob64img;
            _ImageType = type;
        }public ImageFile(ImageType type)
        {
            this.blob64img = null;
            _ImageType = type;
        }

        public ImageFile()
        {
        }
        public ImageFile(string type, string blob64string, ImageType t)
        {
            this.blob64img = "data:image/"+type+";base64,"+blob64string;
            this._ImageType = t;
        }

        public string? Blob64img { get => blob64img; set => blob64img = value; }
        public ImageType ImageType { get => _ImageType; set => _ImageType = value; }
    }
}