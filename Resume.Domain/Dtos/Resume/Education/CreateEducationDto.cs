using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Resume.Education
{
    public class CreateEducationDto
    {
        [Display(Name = "نام دانشگاه / دانشکده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string UnivercityName { get; set; }

        [Display(Name = "تاریخ اغاز تحصیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string EducatioStartDate { get; set; }

        [Display(Name = "تاریخ پایان تحصیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string EducationEndDate { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string Description { get; set; }
    }

    public enum CreateEducationResult
    {
        Success,
        Error
    }
}
