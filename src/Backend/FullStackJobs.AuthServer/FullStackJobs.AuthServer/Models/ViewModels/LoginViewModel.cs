
namespace FullStackJobs.AuthServer.Models.ViewModels
{
    public class LoginViewModel : LoginInputModel
    {
        public bool AllowRememberLogin { get; set; } = true;   
        public bool NewAccount { get; set; }
    }
}
