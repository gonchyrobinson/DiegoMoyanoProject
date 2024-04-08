namespace DiegoMoyanoProject.Models
{
    public class PdfData
    {
        private string path;
        private int order;
        public PdfData() { }
        public PdfData(string path, int order)
        {
            this.path = path;
            this.order = order;
        }
        public string Path { get => path; set => path = value; }
        public int Order { get => order; set => order = value; }
    }
}