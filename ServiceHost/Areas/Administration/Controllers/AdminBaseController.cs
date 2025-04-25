using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("administration")]
    [Authorize]
    public class AdminBaseController : Controller
    {
        protected string SuccessMessage = "Success Message";
        protected string ErrorMessage = "Error Message";
    }
}
