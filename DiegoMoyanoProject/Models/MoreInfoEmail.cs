namespace DiegoMoyanoProject.Models
{
    public class MoreInfoEmail
    {
        private string name;
        private string mail;
        private string title;
        private string body;

        public MoreInfoEmail()
        {
            
        }

        public MoreInfoEmail(string name, string  mail, string title, string body)
        {
            this.Name = name;
            this.Mail = mail;
            this.Title = title;
            this.Body = body;           
        }

        public string Name { get => name; set => name = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Title { get => title; set => title = value; }
        public string Body { get => body; set => body = value; }
    }
}
