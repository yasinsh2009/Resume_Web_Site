using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Project;

namespace ServiceHost.Controllers
{
    public class ProjectController(IProjectService projectService) : SiteBaseController
    {
        public async Task<IActionResult> Projects()
        {
            var project = await projectService.GetAllProjects();
            return View(project);
        }
    }
}
