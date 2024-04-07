using DiegoMoyanoProject.Models;
namespace DiegoMoyanoProject.ViewModels.UserData
{
    public class ViewInversionViewModel
    {
        public int UserId { get; set; }

        public ViewInversionViewModel(Models.User usu, bool isLoguedUser)
        {
            IsAdminOrOwner = usu.Role == Role.Admin || usu.Role == Role.Owner;
            CapitalInvested = usu.CapitalInvested;
            Rentability = usu.Rentability;
            IsLoguedUser = isLoguedUser;
        }

        public bool IsLoguedUser { get; set; }
        public bool IsAdminOrOwner { get; set; }
        public decimal CapitalInvested { get; set; }
        public decimal Rentability { get; set; }
        public ViewInversionViewModel() { }

        public ViewInversionViewModel(int userId, bool isLoguedUser, bool isAdminOrOwner, decimal capitalInvested, decimal rentability)
        {
            UserId = userId;
            IsLoguedUser = isLoguedUser;
            IsAdminOrOwner = isAdminOrOwner;
            CapitalInvested = capitalInvested;
            Rentability = rentability;
        }
    }
}
