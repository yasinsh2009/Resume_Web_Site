using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.User
{
    public class LoginDto
    {
        [Display(Name = "شماره موبایل / ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string Identifier { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string Password { get; set; }
    }

    public enum UserLoginResult
    {
        Success,
        Error,
        UserNotFound
    }
}
