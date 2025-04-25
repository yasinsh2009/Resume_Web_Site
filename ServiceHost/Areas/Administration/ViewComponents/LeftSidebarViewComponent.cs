using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;
using Resume.Domain.IdentityExtentions;

namespace ServiceHost.Areas.Administration.ViewComponents
{
    public class LeftSidebarViewComponent : ViewComponent
    {

        #region Fields

        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public LeftSidebarViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserById(User.GetUserId());

            ViewData["User"] = await _userService.GetUserDetail();

            return View("LeftSidebar");
        }
    }
}
