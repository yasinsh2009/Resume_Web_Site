using System.ComponentModel.DataAnnotations;
using Resume.Domain.Entities.Common;

namespace Resume.Domain.Entities.Blog
{
    public class Article(string title, string? shortDescription, string? description, string? image) : BaseEntity
    {
        public long ArticleCategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string Title { get; set; } = title;

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? ShortDescription { get; set; } = shortDescription;

        [Display(Name = "توضیحات")]
        public string? Description { get; set; } = description;

        [Display(Name = "تصویر مقاله")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} بیشتر باشد")]
        public string? Image { get; set; } = image;

        public ArticleCategory ArticleCategory { get; set; }
    }
}
