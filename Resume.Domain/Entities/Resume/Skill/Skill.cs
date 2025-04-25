using System.ComponentModel.DataAnnotations;
using Resume.Domain.Entities.Common;

namespace Resume.Domain.Entities.Resume.Skill
{
    public class Skill : BaseEntity
    {
        [Display(Name = "عنوان مهارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string SkillTitle { get; set; }

        [Display(Name = "درصد توانایی مهارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(3, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public required string SkillPercent { get; set; }
    }
}
