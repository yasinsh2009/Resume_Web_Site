using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;
using Resume.Application.Tools;
using Resume.Domain.Dtos.User;
using Resume.Domain.Entities.User;
using System.Security.Claims;

namespace ServiceHost.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Fields

        private readonly IUserService _userService;


        #endregion

        #region Constructor

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        #region Login

        [HttpGet("user-login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Administration" });
            }

            return View();
        }

        [HttpPost("user-login")]
        public async Task<IActionResult> Login(LoginDto command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _userService.UserLogin(command);

            switch (result)
            {
                case UserLoginResult.Success:
                    bool IsEmail = ValidatorHelper.IsEmail(command.Identifier);
                    User user;

                    if (IsEmail)
                    {
                        user = await _userService.GetUserByEa(command.Identifier);
                    }
                    else
                    {
                        user = await _userService.GetUserByPn(command.Identifier);
                    }

                    

                    #region Login

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                        new (ClaimTypes.Email, user.EmailAddress)
                    };

                    var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var perncipal = new ClaimsPrincipal(Identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(perncipal, properties);

                    #endregion

                    TempData[SuccessMessage] = $"{user.FullName} عزیز، خوش آمدید";

                    return RedirectToAction("Index", "Home", new { area = "Administration" });
                case UserLoginResult.Error:
                    TempData[ErrorMessage] = "در حین انجام عملیات خطایی رخ داده است، لطفا مجددا تلاش نمایید";
                    break;
                case UserLoginResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربری با مشخصات فوق یافت نشد";
                    break;
            }

            return View();
        }

        #endregion

        #region Logout

        [HttpGet("user-logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #endregion
    }
}
