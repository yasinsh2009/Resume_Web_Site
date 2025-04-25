using Resume.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Resume.Education
{
    public class Education : BaseEntity
    {
        [Display(Name = "نام دانشگاه / دانشکده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string UniversityName { get; set; }

        [Display(Name = "تاریخ اغاز تحصیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string EducationStartDate { get; set; }

        [Display(Name = "تاریخ پایان تحصیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string EducationEndDate { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string Description { get; set; }
    }
}
