using Resume.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.User
{
    public class User
        (string fullName, string phoneNumber, string? emailAddress, string birthPlace, string birthDate, string description, string password, string confirmPassword, string? passwordSalt, bool isBlock, string? avatar)
        : BaseEntity
    {
        #region Properties

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string FullName { get; set; } = fullName;

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string PhoneNumber { get; set; } = phoneNumber;

        [Display(Name = "آدرس ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; } = emailAddress;

        [Display(Name = "محل تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string BirthPlace { get; set; } = birthPlace;

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string BirthDate { get; set; } = birthDate;


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Description { get; set; } = description;

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = password;

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [Compare("Password", ErrorMessage = "رمز های عبور با یکدیگر مغایرت دارند")]
        public string ConfirmPassword { get; set; } = confirmPassword;

        [Display(Name = "نمک")] public string? PasswordSalt { get; set; } = passwordSalt;

        [Display(Name = "بلاک شده / نشده")] public bool IsBlock { get; set; } = isBlock;

        [Display(Name = "آواتار")] public string? Avatar { get; set; } = avatar;

        #endregion
    }
}
