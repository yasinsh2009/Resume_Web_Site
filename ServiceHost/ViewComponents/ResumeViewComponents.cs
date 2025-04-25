using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Education;
using Resume.Application.Services.Interface.Experience;
using Resume.Application.Services.Interface.Skill;

namespace ServiceHost.ViewComponents
{
    #region Education

    public class EducationViewComponent : ViewComponent
    {
        private readonly IEducationService _educationService;

        public EducationViewComponent(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var education = await _educationService.GetAllEducations();
            return View("Education", education);
        }

    }

    #endregion

    #region Experience

    public class ExperienceViewComponent : ViewComponent
    {
        private readonly IExperienceService _experienceService;

        public ExperienceViewComponent(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var exp = await _experienceService.GetAllExperiences();
            return View("Experience", exp);
        }

    }

    #endregion

    #region Skill

    public class SkillViewComponent : ViewComponent
    {
        private readonly ISkillService _skillService;

        public SkillViewComponent(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var skill = await _skillService.GetAllSkills();
            return View("Skill", skill);
        }

    }

    #endregion
}
