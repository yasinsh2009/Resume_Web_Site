using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Project.ProjectCategory
{
    public class CreateProjectCategoryDto
    {

        [Display(Name = "عنوان دسته بندی نمونه کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? Description { get; set; }
    }

    public enum CreateProjectCategoryResult
    {
        Success,
        Error,
        Failed
    }
}
