using Microsoft.AspNetCore.Http;
using Resume.Domain.Dtos.Blog.Article;
using Resume.Domain.Dtos.Blog.ArticleCategory;
using Resume.Domain.Dtos.Contact;

namespace Resume.Application.Services.Interface.Blog;

public interface IBlogService : IAsyncDisposable
{
    #region Article - Category

    Task<List<FilterArticleCategoriesDto>> GetAllArticleCategories();
    Task<CreateArticleCategoryResult> CreateArticleCategory(CreateArticleCategoryDto command, IFormFile image);
    Task<object> GetAllCategories();

    #endregion

    #region Article

    Task<List<FilterArticlesDto>> GetAllArticles();
    Task<CreateArticleResult> CreateArticle(CreateArticleDto command, IFormFile image);

    #endregion
}