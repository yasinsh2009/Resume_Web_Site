using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Dtos.User;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class UserController : AdminBaseController
    {
        #region Fields

        private readonly IUserService _userService;

        //private static readonly List<ProvinceUserDto> Provinces = new List<ProvinceUserDto>
        //{

        //};

        //private static readonly List<CityUserDto> Cities = new List<CityUserDto>
        //{

        //};

        #endregion

        #region Conctructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost]
        //public JsonResult Getprovinces(long countryId)
        //{
        //    var provinces = Provinces.Where(x => x.CountryId == countryId).ToList();
        //    return Json(provinces);
        //}

        //[HttpPost]
        //public JsonResult GetCities(long provinceId)
        //{
        //    var cities = Cities.Where(x => x.ProvinceId == provinceId).ToList();
        //    return Json(cities);
        //}

        #endregion

        #region Actions

        #region User - List

        [HttpGet("users-list")]
        public async Task<IActionResult> UserList(FilterUserDto filter, string FullName, string PhoneNumber)
        {
            filter.FullName = FullName;
            filter.PhoneNumber = PhoneNumber;
            var users = await _userService.GetAllUsers(filter);
            return View(users);
        }

        #endregion

        #region Create - User

        [HttpGet("create-user")]
        public IActionResult CreateUser()
        {
            //ViewBag.Countries = Countries;
            return View();
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(RegisterUserDto user, IFormFile avatar)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.CreateUser(user, avatar);

            switch (result)
            {
                case CreateUserResult.DuplicatePhoneNumber:
                    TempData["Duplicate"] = "شماره تلفن شما تکراری می‌باشد";
                    return View();
                case CreateUserResult.Success:
                    TempData["Success"] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("UserList", "User", new { area = "administration" });
                case CreateUserResult.Error:
                    return View(user);
                default:
                    return RedirectToPage("NotFoundPage", "Home", "Administrarion");
            }
        }

        #endregion

        #region Edit - User

        [HttpGet("edit-user/{Id}")]
        public async Task<IActionResult> EditUser(long Id)
        {
            var user = await _userService.GetForEditUser(Id);

            //ViewBag.Countries = Countries;
            ViewBag.FullName = user.FullName;

            if (user == null)
            {
                return NotFound("کاربر موردنظر یافت نشد.");
            }

            return View(user);
        }

        [HttpPost("edit-user/{Id}")]
        public async Task<IActionResult> EditUser(UpdateUserDto editUser, IFormFile avatar)
        {

            if (!ModelState.IsValid)
            {
                return View(); // بازگشت فرم با داده‌های موجود
            }

            var result = await _userService.EditUser(editUser,avatar);

            switch (result)
            {
                case UpdateUserResult.Success:
                    TempData["SuccessMessage"] = "کاربر با موفقیت ویرایش شد.";
                    return RedirectToAction("UserList", "User", new { area = "administration" });
                case UpdateUserResult.NotFoundUser:
                    return NotFound("کاربر یافت نشد.");
                case UpdateUserResult.Error:
                    ModelState.AddModelError("", "خطایی در عملیات رخ داده است.");
                    return View(editUser);
            }

            return View();
        }


        #endregion

        #region Block - & - Unblock - User

        [HttpGet("block-user/{Id}")]
        public async Task<IActionResult> BlockUser(long Id)
        {
            var user = await _userService.BlockUser(Id);

            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction("UserList", "User", new { area = "administration" });
        }

        [HttpGet("unblock-user/{Id}")]
        public async Task<IActionResult> UnblockUser(long Id)
        {
            var user = await _userService.UnblockUser(Id);

            if (user == null)
            {
                return NotFound();
            }

            return RedirectToAction("UserList", "User", new { area = "administration" });
        }

        #endregion

        #region User - Place

        //[HttpGet]
        //public IActionResult GetCountries()
        //{
        //    var countries = new List<UserCountry>
        //    {
        //        new UserCountry{Id = 1, CountryName = "ایران"},
        //        new UserCountry{Id = 2, CountryName = "امریکا"},
        //        new UserCountry{Id = 3, CountryName = "اسپانیا"}
        //    };

        //    return Json(countries);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetProvices(long countryId)
        //{
        //    var provinces = await Task.FromResult( new List<UserProvince>
        //    {
        //        new UserProvince {Id = 1, ProvinceName = "آذربایجان غربی", CountryId = 1},
        //        new UserProvince {Id = 2, ProvinceName = "آذربایجان شرقی", CountryId = 1},
        //        new UserProvince { Id = 3, ProvinceName = "کردستان", CountryId = 1 },
        //        new UserProvince { Id = 4, ProvinceName = "کالیفرنیا", CountryId = 2 },
        //        new UserProvince { Id = 5, ProvinceName = "تگراس", CountryId = 2 },
        //        new UserProvince { Id = 6, ProvinceName = "فلوریدا", CountryId = 2 },
        //        new UserProvince { Id = 7, ProvinceName = "مادرید", CountryId = 3 },
        //        new UserProvince { Id = 8, ProvinceName = "بارسلونا", CountryId = 3 },
        //        new UserProvince { Id = 9, ProvinceName = "خیرونا", CountryId = 3 }
        //    });

        //    var FilteredProvinces = provinces.Where(x => x.CountryId == countryId).ToList();
        //    return Json(FilteredProvinces);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetCiteis(long provinceId)
        //{
        //    var cities = await Task.FromResult(new List<UserCity>
        //    {
        //    new UserCity { Id = 1, CityName = "بوکان", ProvinceId = 1},
        //    new UserCity { Id = 1, CityName = "تکاب", ProvinceId = 1},
        //    new UserCity { Id = 1, CityName = "شاهین‌دژ", ProvinceId = 1 },
        //    new UserCity { Id = 2, CityName = "تبریز", ProvinceId = 2 },
        //    new UserCity { Id = 2, CityName = "ملکان", ProvinceId = 2 },
        //    new UserCity { Id = 2, CityName = "مراغه", ProvinceId = 2 },
        //    new UserCity { Id = 1, CityName = "سنندج", ProvinceId = 3 },
        //    new UserCity { Id = 2, CityName = "بانه", ProvinceId = 3 },
        //    new UserCity { Id = 3, CityName = "سقز", ProvinceId = 3 },
        //    new UserCity { Id = 1, CityName = "لس‌آنجلس", ProvinceId = 4 },
        //    new UserCity { Id = 2, CityName = "سان‌فرانسیسکو", ProvinceId = 4 },
        //    new UserCity { Id = 3, CityName = "سن‌دیگو", ProvinceId = 4 },
        //    new UserCity { Id = 1, CityName = "آستین‌", ProvinceId = 5 },
        //    new UserCity { Id = 2, CityName = "سن‌آنتونیو", ProvinceId = 5 },
        //    new UserCity { Id = 3, CityName = "‌هیوستون", ProvinceId = 5 },
        //    new UserCity { Id = 1, CityName = "میامی‌", ProvinceId = 6 },
        //    new UserCity { Id = 2, CityName = "‌هالیوود", ProvinceId = 6 },
        //    new UserCity { Id = 3, CityName = "‌بانل", ProvinceId = 6 },
        //    new UserCity { Id = 1, CityName = "ختافه‌", ProvinceId = 7 },
        //    new UserCity { Id = 2, CityName = "‌برونته", ProvinceId = 7 },
        //    new UserCity { Id = 3, CityName = "‌آلپدرته", ProvinceId = 7 },
        //    new UserCity { Id = 1, CityName = "لارامبلا‌", ProvinceId = 8 },
        //    new UserCity { Id = 2, CityName = "‌پاسیج‌دگرسیا", ProvinceId = 8 },
        //    new UserCity { Id = 3, CityName = "‌پورتال‌دآنجل", ProvinceId = 8 },
        //    new UserCity { Id = 1, CityName = "اوسونا‌", ProvinceId = 9 },
        //    new UserCity { Id = 2, CityName = "‌کارمونا‌", ProvinceId = 9 },
        //    new UserCity { Id = 3, CityName = "‌‌ارهال", ProvinceId = 9 },
        //    });

        //    var FilteredCities = cities.Where(x => x.ProvinceId == provinceId).ToList();
        //    return Json(FilteredCities);
        //}

        #endregion

        #endregion
    }
}
