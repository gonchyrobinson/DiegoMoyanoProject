using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.ViewModels.User
{
    public class IndexUserViewModel
    {
        public IndexUserViewModel(List<UserOfIndexUserViewModel> listOfUsers, Role role, int loguedUserId)
        {
            ListOfUsers = listOfUsers;
            IsAdminOROwner = ((role == Role.Admin) || (role == Role.Owner));
            LoguedUserId = loguedUserId;    
        }

        public IndexUserViewModel()
        {
            ListOfUsers = new List<UserOfIndexUserViewModel>();
        }

        public List<UserOfIndexUserViewModel> ListOfUsers { get; set; }
        public bool IsAdminOROwner { get; set; }
        public int LoguedUserId { get; set; }
    }
}
