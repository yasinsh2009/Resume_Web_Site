using Resume.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Project
{
    public class Project(string projectImage, string projectTitle, string description) : BaseEntity
    {
        #region Properties

        public long ProjectCategoryId { get; set; }

        [Display(Name = "تصویر پروژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string ProjectImage { get; set; } = projectImage;

        [Display(Name = "عنوان پروژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string ProjectTitle { get; set; } = projectTitle;

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Description { get; set; } = description;

        #endregion

        #region Relations

        public ProjectCategory ProjectCategory { get; set; }

        #endregion
    }
}