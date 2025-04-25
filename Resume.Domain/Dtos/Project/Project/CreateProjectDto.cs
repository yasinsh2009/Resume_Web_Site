using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Project.Project
{
    public class CreateProjectDto
    {

        public long ProjectCategoryId { get; set; }

        [Display(Name = "عنوان پروژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(250, ErrorMessage = "{0} نمی‌تواند از {1} بیشتر باشد")]
        public required string ProjectTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(450, ErrorMessage = "{0} نمی‌تواند از {1} بیشتر باشد")]
        public required string? Description { get; set; }
    }

    public class CreateProjectResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static CreateProjectResult Success() => new CreateProjectResult(true);
        public static CreateProjectResult Failed(string message) => new CreateProjectResult(false, message);
    }
}
