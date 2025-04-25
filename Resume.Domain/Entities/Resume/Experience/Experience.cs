using System.ComponentModel.DataAnnotations;
using Resume.Domain.Entities.Common;

namespace Resume.Domain.Entities.Resume.Experience
{
    public class Experience : BaseEntity
    {
        [Display(Name = "نام شرکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string CompanyName { get; set; }

        [Display(Name = "تاریخ اغاز کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string JobStartDate { get; set; }

        [Display(Name = "تاریخ پایان کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string JobEndDate { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string Description { get; set; }
    }
}
