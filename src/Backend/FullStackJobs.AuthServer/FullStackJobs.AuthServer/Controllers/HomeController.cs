using Microsoft.AspNetCore.Mvc;

namespace FullStackJobs.AuthServer.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
