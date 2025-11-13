using MarketPlace.Application.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.Blog;
using Resume.Domain.Dtos.Blog.Article;
using Resume.Domain.Dtos.Blog.ArticleCategory;
using Resume.Domain.Entities.Blog;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.Blog
{
    public class BlogService : IBlogService
    {
        #region Fields

        private readonly IGenericRepository<ArticleCategory> _articleCategoryRepository;
        private readonly IGenericRepository<Article> _articleRepository;

        #endregion

        #region Constructor

        public BlogService(IGenericRepository<ArticleCategory> articleCategoryRepository, IGenericRepository<Article> articleRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _articleRepository = articleRepository;
        }

        #endregion

        #region Article - Category

        #region Filter - Article - Category

        public async Task<List<FilterArticleCategoriesDto>> GetAllArticleCategories()
        {
            return await _articleCategoryRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterArticleCategoriesDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Image = x.Image,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<object> GetAllCategories()
        {
            return await _articleCategoryRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(c => new { c.Id, c.Title }).OrderByDescending(x => x.Id)
                .ToListAsync();

        }

        #endregion

        #region Create - Article - Category

        public async Task<CreateArticleCategoryResult> CreateArticleCategory(CreateArticleCategoryDto command, IFormFile image)
        {
            if (command == null)
            {
                return CreateArticleCategoryResult.Failed("داده‌های ورودی نامعتبر است.");
            }

            try
            {
                // تعیین مسیر ذخیره‌سازی تصویر
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "articleCategory");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(uploadDirectory, fileName);

                // ذخیره فایل به‌صورت آسنکرون
                await using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                // ایجاد دسته بندی مقاله جدید
                var newArticleCategory =
                    new ArticleCategory(command.Title, command.ShortDescription, command.Description, fileName);

                await _articleCategoryRepository.AddEntity(newArticleCategory);
                await _articleCategoryRepository.SaveChanges();

                return CreateArticleCategoryResult.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating article category: {ex.Message}");
                return CreateArticleCategoryResult.Failed("خطایی در ایجاد دسته بندی مقالات رخ داد.");
            }
        }

        #endregion

        #endregion

        #region Article

        #region Filter - Article

        public async Task<List<FilterArticlesDto>> GetAllArticles()
        {
            return await _articleRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Include(x => x.ArticleCategory)
                .Select(x => new FilterArticlesDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    ArticleCategoryTitle = x.ArticleCategory.Title,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Image = x.Image,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        #endregion

        #region Create - Article

        public async Task<CreateArticleResult> CreateArticle(CreateArticleDto command, IFormFile image)
        {
            if (command == null)
            {
                return CreateArticleResult.Failed("داده‌های ورودی نامعتبر است.");
            }

            try
            {
                string fileName = null;
                if (image is not null)
                {
                    // تعیین مسیر ذخیره‌سازی تصویر
                    var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "article");
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    var filePath = Path.Combine(uploadDirectory, fileName);

                    // ذخیره فایل به‌صورت آسنکرون
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                }

                // ایجاد مقاله جدید
                var newArticle = new Article
                {
                    ArticleCategoryId = command.ArticleCategoryId,
                    Title = command.Title,
                    ShortDescription= command.ShortDescription,
                    Description = command.Description,
                    Image = fileName
                };

                await _articleRepository.AddEntity(newArticle);
                await _articleRepository.SaveChanges();

                return CreateArticleResult.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating article category: {ex.Message}");
                return CreateArticleResult.Failed("خطایی در ایجاد مقاله رخ داد.");
            }
        }

        #endregion

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_articleCategoryRepository != null)
            {
                await _articleCategoryRepository.DisposeAsync();
            }
        }

        #endregion
    }
}
