using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using Resume.Application.Services.Interface.Project;
using Resume.Domain.Dtos.Project.Project;
using Resume.Domain.Dtos.Project.ProjectCategory;
using Resume.Domain.Dtos.Resume.Skill;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class AdminProjectsController(IProjectService projectService) : AdminBaseController
    {
        #region Project - Categories

        #region Get - All - Project - Categories

        [HttpGet("project-categories-list")]
        public async Task<IActionResult> FilterProjectCategories()
        {
            var projectCategory = await projectService.GetAllProjectCategories();
            return View(projectCategory);
        }

        #endregion

        #region Create - Project - Category

        [HttpGet("create-project-category")]
        public IActionResult CreateProjectCategory()
        {
            return View();
        }

        [HttpPost("create-project-category"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProjectCategory(CreateProjectCategoryDto projectCategory,
            IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await projectService.CreateProjectCategory(projectCategory, image);

            switch (result)
            {
                case CreateProjectCategoryResult.Success:
                    TempData[("SuccessMessage")] = "دسته بندی نمونه کار با موفقیت ایجاد شد.";
                    return RedirectToAction("FilterProjectCategories", "AdminProjects",
                        new { area = "Administration" });
                case CreateProjectCategoryResult.Error:
                    TempData["ErrorMessage"] = "ایجاد دسته بندی پروژه با خطا مواجه شد.";
                    return View(projectCategory);
            }

            return View();
        }
    

    #endregion

        #region Edit - Project - Category

        [HttpGet("edit-project-category/{id}")]
        public async Task<IActionResult> EditProjectCategory(long id)
        {
            var projectCategory = await projectService.GetProjectCategoryForEdit(id);

            ViewBag.Title = projectCategory.Title;

            if (projectCategory == null)
            {
                return NotFound();
            }

            return View(projectCategory);
        }

        [HttpPost("edit-project-category/{id}")]
        public async Task<IActionResult> EditProjectCategory(EditProjectCategoryDto projectCategory, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await projectService.EditProjectCategory(projectCategory, image);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "دسته بندی نمونه کار با موفقیت ویرایش شد.";
                return RedirectToAction("FilterProjectCategories",
                    "AdminProjects",
                    new { area = "Administration" });
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "ویرایش دسته بندی نمونه کار با خطا مواجه شد.";
                return View(projectCategory);
            }
        }

        #endregion

        #endregion

        #region Projects

        #region Get - All - Projects - Item

        [HttpGet("projects-list")]
        public async Task<IActionResult> FilterProjects()
        {
            var project = await projectService.GetAllProjects();
            return View(project);
        }

        #endregion

        #region Create - Project

        [HttpGet("create-project")]
        public async Task<IActionResult> CreateProject()
        {
            var projectCategory = await projectService.GetProjectCategories();
            ViewBag.Category = projectCategory;

            return View();
        }

        [HttpPost("create-project"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(CreateProjectDto project, IFormFile projectImage)
        {
            var projectCategory = projectService.GetProjectCategories();
            ViewBag.Category = projectCategory;

            var result = await projectService.CreateProject(project, projectImage);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "پروژه با موفقیت ایجاد شد.";
                return RedirectToAction("FilterProjects", "AdminProjects", new { area = "Administration" });
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "خطایی در ایجاد پروژه رخ داد.";
            }

            return View(project);
        }


        #endregion

        #region Edit - Project

        [HttpGet("edit-project/{id}")]
        public async Task<IActionResult> EditProject(long id)
        {
            var project = await projectService.GetProjectForEdit(id);

            ViewBag.Title = project.ProjectTitle;

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost("edit-project/{id}")]
        public async Task<IActionResult> EditProject(EditProjectDto project, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await projectService.EditProject(project, image);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "نمونه کار با موفقیت ویرایش شد.";
                return RedirectToAction("FilterProjectCategories",
                    "AdminProjects",
                    new { area = "Administration" });
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "ویرایش نمونه کار با خطا مواجه شد.";
                return View(project);
            }
        }

        #endregion

        #endregion
    }
}
