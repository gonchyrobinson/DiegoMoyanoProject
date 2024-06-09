namespace DiegoMoyanoProject.Models
{
    public class PdfData
    {
        private byte[] pdf;
        public PdfData() { }
        public PdfData(byte[] pdf)
        {
            this.pdf = pdf;
        }
        public byte[] Pdf { get => pdf; set => pdf = value; }
    }
}