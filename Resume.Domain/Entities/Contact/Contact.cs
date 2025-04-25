using System.ComponentModel.DataAnnotations;
using Resume.Domain.Entities.Common;

namespace Resume.Domain.Entities.Contact
{
    public class Contact(string? location, string fullname, string? emailAddress, string message) : BaseEntity
    {
        [Display(Name = "موقعیت مکانی")]
        public string? Location { get; set; } = location;

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Fullname { get; set; } = fullname;

        [Display(Name = "آدرس ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; } = emailAddress;

        [Display(Name = "پیغام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(600, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Message { get; set; } = message;
    }

}
