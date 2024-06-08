namespace DiegoMoyanoProject.ViewModels.UserPdf
{
    public class PruebaImgViewModel
    {
        public PruebaImgViewModel() { } 
        public string Img { get; set; }

        public PruebaImgViewModel(byte[] img)
        {
            string base64String = Convert.ToBase64String(img); // Convert to base64
            this.Img = "data:image/jpg;base64," + base64String; // Create a data URL with the base64 string
        }
    }
}
