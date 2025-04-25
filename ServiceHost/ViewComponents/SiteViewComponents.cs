using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;

namespace ServiceHost.ViewComponents
{
    #region SiteHeader

    public class SiteHeaderViewComponent(IUserService userService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userService.GetUserDetail();
            return View("SiteHeader", user);
        }
    }

    #endregion

    #region SiteFooter

    public class SiteFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("SiteFooter");
        }
    }

    #endregion
}