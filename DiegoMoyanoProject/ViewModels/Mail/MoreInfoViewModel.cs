using System.ComponentModel.DataAnnotations;

namespace DiegoMoyanoProject.ViewModels.Mail
{
    public class MoreInfoViewModel
    {

        public MoreInfoViewModel(string name, string email, string title, string body)
        {
            Name = name;
            Mail = email;
            Title = title;
            Body = body;
        }

        public MoreInfoViewModel(string title, string body)
        {
            Name = "";
            Mail = "";
            Title = title;
            Body = body;
        }

        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Mail Address")]
        public string Mail { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage ="Este campo es requerido")]
        public string Title { get; set; }
        [Display(Name ="Body")]
        [Required(ErrorMessage="Este campo es requerido")]
        public string Body { get; set; }

    }
}
