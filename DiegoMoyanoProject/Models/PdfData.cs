namespace DiegoMoyanoProject.Models
{
    public class PdfData
    {
        private byte[] pdf;
        private int order;
        public PdfData() { }
        public PdfData(byte[] pdf, int order)
        {
            this.pdf = pdf;
            this.order = order;
        }
        public byte[] Pdf { get => pdf; set => pdf = value; }
        public int Order { get => order; set => order = value; }
    }
}