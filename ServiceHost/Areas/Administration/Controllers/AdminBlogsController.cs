using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Blog;
using Resume.Domain.Dtos.Blog.Article;
using Resume.Domain.Dtos.Blog.ArticleCategory;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class AdminBlogsController(IBlogService blogService) : AdminBaseController
    {
        #region Article - Category

        #region Get - All - ArticleCtgies - Item

        [HttpGet("article-category-list")]
        public async Task<IActionResult> FilterArticleCategories()
        {
            var articleCategories = await blogService.GetAllArticleCategories();
            return View(articleCategories);
        }

        #endregion

        #region Create - Article - Category

        [HttpGet("create-article-category")]
        public IActionResult CreateArticleCategory()
        {
            return View();
        }

        [HttpPost("create-article-category"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticleCategory(CreateArticleCategoryDto articleCategory, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
                return View(articleCategory);
            }

            var result = await blogService.CreateArticleCategory(articleCategory, Image);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "پروژه با موفقیت ایجاد شد.";
                return RedirectToAction("FilterArticleCategories", "AdminBlogs", new { area = "Administration" });
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "خطایی در ایجاد پروژه رخ داد.";
                return View(articleCategory);
            }
        }


        #endregion

        #endregion

        #region Article

        #region Get - All - Article - Item

        [HttpGet("article-list")]
        public async Task<IActionResult> FilterArticles()
        {
            var articles = await blogService.GetAllArticles();
            return View(articles);
        }

        #endregion

        #region Create - Article

        [HttpGet("create-article")]
        public async Task<IActionResult> CreateArticle()
        {
            var articleCategory = await blogService.GetAllCategories();
            ViewBag.Category = articleCategory;

            return View();
        }

        [HttpPost("create-article"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(CreateArticleDto article, IFormFile image)
        {
            var articleCategory = await blogService.GetAllCategories();
            ViewBag.Category = articleCategory;

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
                return View(article);
            }

            var result = await blogService.CreateArticle(article, image);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "مقاله با موفقیت ایجاد شد.";
                return RedirectToAction("FilterArticles", "AdminBlogs", new { area = "Administration" });
            }

            TempData["ErrorMessage"] = result.Message ?? "خطایی در ایجاد پروژه رخ داد.";
            return View(article);
        }


        #endregion

        #endregion
    }
}
