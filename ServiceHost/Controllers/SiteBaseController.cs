using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class SiteBaseController : Controller
    {
        protected string SuccessMessage = "Success Message";
        protected string ErrorMessage = "Error Message";
    }
}
