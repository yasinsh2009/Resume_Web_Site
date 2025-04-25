using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Blog.ArticleCategory
{
    public class CreateArticleCategoryDto
    {
        [Display(Name = "عنوان دسته بندی مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? Description { get; set; }
    }

    public class CreateArticleCategoryResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static CreateArticleCategoryResult Success() => new CreateArticleCategoryResult(true);
        public static CreateArticleCategoryResult Failed(string message) => new CreateArticleCategoryResult(false, message);
    }
}
