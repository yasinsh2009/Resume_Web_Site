using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Contact
{
    public class CreateContactMessageDto
    {
        [Display(Name = "موقعیت مکانی")]
        public string? Location { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Fullname { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Display(Name = "پیغام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(600, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Message { get; set; }
    }
    public class CreateContactMessageResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static CreateContactMessageResult Success() => new CreateContactMessageResult(true);
        public static CreateContactMessageResult Failed(string message) => new CreateContactMessageResult(false, message);
    }
}
