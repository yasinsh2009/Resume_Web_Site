using Resume.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Project
{
    public class ProjectCategory(string title, string? description, string? image) : BaseEntity
    {
        #region Properties

        [Display(Name = "عنوان دسته بندی نمونه کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Title { get; set; } = title;

        [Display(Name = "توضیحات")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? Description { get; set; } = description;
        
        [Display(Name = "تصویر دسته بندی نمونه کار")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? Image { get; set; } = image;

        #endregion

        #region Relations

        public ICollection<Project> Projects { get; set; }

        #endregion
    }
}
