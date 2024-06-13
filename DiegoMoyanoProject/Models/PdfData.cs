namespace DiegoMoyanoProject.Models
{
    public class PdfData
    {
        private byte[] pdf;
        private int id;
        public PdfData() { }
        public PdfData(byte[] pdf, int id)
        {
            this.pdf = pdf;
            this.id = id;
        }
        public byte[] Pdf { get => pdf; set => pdf = value; }
        public int Id { get => id; set => id = value; }
    }
}