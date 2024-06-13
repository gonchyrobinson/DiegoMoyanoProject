namespace DiegoMoyanoProject.Models
{
    public class FileDate
    {
        private int id;
        private DateTime date;

        public int Id { get => id; set => id = value; }
        public DateTime Date { get => date; set => date = value; }

        public FileDate(int id, DateTime date)
        {
            this.id = id;
            this.date = date;
        }

        public FileDate()
        {
            //I put -1, because i consider that will never exist
            this.Id = -1;
            this.date = DateTime.Today;
        }
    }
}
