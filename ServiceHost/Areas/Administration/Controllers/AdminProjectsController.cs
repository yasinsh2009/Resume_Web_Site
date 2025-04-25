using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Project;
using Resume.Domain.Dtos.Project.Project;
using Resume.Domain.Dtos.Project.ProjectCategory;
using Resume.Domain.Dtos.Resume.Skill;

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

        #region Create - Project - Categories

        [HttpGet("create-project-category")]
        public IActionResult CreateProjectCategory()
        {
            return View();
        }

        [HttpPost("create-project-category"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProjectCategory(CreateProjectCategoryDto projectCategory, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                TempData[("ErrorMessage")] = "لطفا فیلد های لازم را به درستی پر کنید.";
                return View(projectCategory);
            }

            var result = await projectService.CreateProjectCategory(projectCategory, image);

            if (result.IsSuccess)
            {
                TempData[("SuccessMessage")] = "دسته بندی نمونه کار با موفقیت ایجاد شد.";
                return RedirectToAction("FilterProjectCategories", "AdminProjects",
                    new { area = "Administration" });
            }

            TempData["ErrorMessage"] = result.Message ?? "ایجاد دسته بندی پروژه با خطا مواجه شد.";
            return View(result);
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
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost("create-project"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(CreateProjectDto project, IFormFile projectImage)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
                return View(project);
            }

            var result = await projectService.CreateProject(project, projectImage);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "پروژه با موفقیت ایجاد شد.";
                return RedirectToAction("FilterProjects", "AdminProjects", new { area = "Administration" });
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "خطایی در ایجاد پروژه رخ داد.";
                return View(project);
            }
        }


#endregion

        #endregion
    }
}
