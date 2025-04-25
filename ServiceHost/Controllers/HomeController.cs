using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Resume.Domain.Dtos.User;

namespace ServiceHost.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly ILogger<HomeController> _logger;
        

        public IActionResult Index()
        {
            return View();
        }
    }
}
