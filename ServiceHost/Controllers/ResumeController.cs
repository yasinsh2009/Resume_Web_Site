using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult HomeResume()
        {
            return View();
        }
    }
}
