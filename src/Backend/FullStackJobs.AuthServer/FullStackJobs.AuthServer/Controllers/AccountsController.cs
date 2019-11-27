using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; 
using FullStackJobs.AuthServer.Models;
using FullStackJobs.AuthServer.Infrastructure.Data.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FullStackJobs.AuthServer.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public AccountsController(UserManager<AppUser> userManager, AppIdentityDbContext appIdentityDbContext)
        {
            _userManager = userManager;
            _appIdentityDbContext = appIdentityDbContext;
        }

        [Route("api/[controller]")]
        public async Task<IActionResult> Post(SignupRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = model.Email, FullName = model.FullName, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("name", user.FullName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", model.Role));

            // ToDo: resotre when signup view introduced in client dev
            // Insert in entity table
            // var commandText = $"INSERT {model.Role + "s"} (Id,Created,FullName) VALUES (@Id,getutcdate(),@FullName)";
            // var id = new SqlParameter("@Id", user.Id);
            // var name = new SqlParameter("@FullName", user.FullName);
            // await _appIdentityDbContext.Database.ExecuteSqlRawAsync(commandText, id, name);

            return Ok(new SignupResponse(user, model.Role));
        }         
    }
}
