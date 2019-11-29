using FullStackJobs.AuthServer.Infrastructure.Data.Identity;

namespace FullStackJobs.AuthServer.Models
{
    public class SignupResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public SignupResponse() { }

        public SignupResponse(AppUser user, string role)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Role = role;
        }
    }
}
