using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.User
{
    public class RegisterUserDto
    {
        #region Properties

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string FullName { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Display(Name = "محل تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string BirthPlace { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string BirthDate { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Description { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [Compare("Password", ErrorMessage = "رمز های عبور با یکدیگر مغایرت دارند")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "نمک")]
        public string? PasswordSalt { get; set; }

        [Display(Name = "بلاک شده / نشده")]
        public bool IsBlock { get; set; }

        [Display(Name = "آواتار")]
        public string? Avatar { get; set; }

        #endregion
    }
    #region Create - User - Result

    public enum CreateUserResult
    {
        Success,
        Error,
        DuplicatePhoneNumber
    }

    #endregion
}
