namespace Resume.Domain.Dtos.Blog.Article
{
    public class FilterArticlesDto
    {
        public long Id { get; set; }
        public string ArticleCategoryTitle { get; set; }
        public string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string CreateDate { get; set; }
    }
}
