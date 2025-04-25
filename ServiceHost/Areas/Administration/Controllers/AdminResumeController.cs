using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Education;
using Resume.Application.Services.Interface.Experience;
using Resume.Application.Services.Interface.Skill;
using Resume.Domain.Dtos.Resume.Education;
using Resume.Domain.Dtos.Resume.Experience;
using Resume.Domain.Dtos.Resume.Skill;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class AdminResumeController : AdminBaseController
    {
        #region Fields

        private readonly IEducationService _educationService;
        private readonly IExperienceService _experienceService;
        private readonly ISkillService _skillService;

        #endregion

        #region Condtructor
        public AdminResumeController(IEducationService educationService, IExperienceService experienceService, ISkillService skillService)
        {
            _educationService = educationService;
            _experienceService = experienceService;
            _skillService = skillService;
        }

        #endregion

        #region Actions

        #region Education

        #region Get - All - Educations - Item

        [HttpGet("education-list")]
        public async Task<IActionResult> FilterEducations()
        {
            var education = await _educationService.GetAllEducations();
            return View(education);
        }

        #endregion

        #region Create - Skill

        [HttpGet("create-education")]
        public IActionResult CreateEducation()
        {
            return View();
        }

        [HttpPost("create-education"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEducation(CreateEducationDto education)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _educationService.CreateEducation(education);

            switch (result)
            {
                case CreateEducationResult.Success:
                    return RedirectToAction("FilterEducations", "AdminResume", new { area = "Administration" });
                case CreateEducationResult.Error:
                    return (IActionResult)(TempData["ErrorMessage"] = ErrorMessage);
            }

            return View(education);
        }

        #endregion

        #region Edit - Education

        [HttpGet("edit-education/{id}")]
        public async Task<IActionResult> EditEducation(long id)
        {
            var education = await _educationService.GetEducationForEdit(id);

            ViewBag.univercityName = education.UnivercityName;

            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        [HttpPost("edit-education/{id}")]
        public async Task<IActionResult> EditEducation(EditEducationDto education)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _educationService.EditEducation(education);

            switch (result)
            {
                case EditEducationResult.Success:
                    TempData["SuccessMessage"] = "مقطع تحصیلی با موفقیت ویرایش شد.";
                    return RedirectToAction("FilterEducations",
                        "AdminResume",
                        new { area = "Administration"});
                case EditEducationResult.Error:
                    ModelState.AddModelError("","در عملیات ویرایش خطایی رخ داد");
                    return View(education);
                case EditEducationResult.NotFoundEducation:
                    return NotFound("مقطع تحصیلی یافت نشد.");
            }
            
            return View(education);
        }

        #endregion

        #endregion

        #region Experience

        #region Get - All - Experiences - Item

        [HttpGet("experiences-list")]
        public async Task<IActionResult> FilterExperiences()
        {
            var exp = await _experienceService.GetAllExperiences();
            return View(exp);
        }

        #endregion

        #region Create - Experience

        [HttpGet("create-experience")]
        public IActionResult CreateExperience()
        {
            return View();
        }

        [HttpPost("create-experience"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience(CreateExperienceDto exp)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _experienceService.CreateExperience(exp);

            switch (result)
            {
                case CreateExperienceResult.Success:
                    return RedirectToAction("FilterExperiences", "AdminResume", new { area = "Administration" });
                case CreateExperienceResult.Error:
                    return (IActionResult)(TempData["ErrorMessage"] = ErrorMessage);
            }

            return View(exp);
        }

        #endregion

        #region Edit - Experience

        [HttpGet("edit-experience/{id}")]
        public async Task<IActionResult> EditExperience(long id)
        {
            var exp = await _experienceService.GetExperienceForEdit(id);

            ViewBag.companyName = exp.CompanyName;

            if (exp == null)
            {
                return NotFound();
            }

            return View(exp);
        }

        [HttpPost("edit-experience/{id}")]
        public async Task<IActionResult> EditExperience(EditExperienceDto exp)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _experienceService.EditExperience(exp);

            switch (result)
            {
                case EditExperienceResult.Success:
                    TempData["SuccessMessage"] = "تجربه کاری با موفقیت ویرایش شد";
                    return RedirectToAction("FilterExperiences",
                        "AdminResume",
                        new { area = "Administration" });
                case EditExperienceResult.Error:
                    ModelState.AddModelError("", "خطایی در عملیات رخ داد.");
                    return View(exp);
                case EditExperienceResult.NotFoundExperience:
                    return NotFound("تجربه کاری موردنظر یافت نشد");
            }

            return View(exp);
        }

        #endregion

        #endregion

        #region Skill

        #region Get - All - Skills - Item

        [HttpGet("skills-list")]
        public async Task<IActionResult> FilterSkills()
        {
            var skill = await _skillService.GetAllSkills();
            return View(skill);
        }

        #endregion

        #region Create - Skill

        [HttpGet("create-skill")]
        public IActionResult CreateSkill()
        {
            return View();
        }

        [HttpPost("create-skill"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSkill(CreateSkillDto skill)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _skillService.CreateSkill(skill);

            switch (result)
            {
                case CreateSkillResult.Success:
                    return RedirectToAction("FilterSkills", "AdminResume", new { area = "Administration" });
                case CreateSkillResult.Error:
                    return (IActionResult)(TempData["ErrorMessage"] = ErrorMessage);
            }

            return View(skill);
        }

        #endregion

        #region Edit - Skill

        [HttpGet("edit-skill/{id}")]
        public async Task<IActionResult> EditSkill(long id)
        {
            var skill = await _skillService.GetSkillForEdit(id);

            ViewBag.skillTitle = skill.SkillTitle;

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        [HttpPost("edit-skill/{id}")]
        public async Task<IActionResult> EditSkill(EditSkillDto skill)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _skillService.EditSkill(skill);

            switch (result)
            {
                case EditSkillResult.Success:
                    TempData["SuccessMessage"] = "مهارت با موفقیت ویرایش شد";
                    return RedirectToAction("FilterSkills",
                        "AdminResume",
                        new { area = "Administration" });
                case EditSkillResult.Error:
                    ModelState.AddModelError("", "خطایی در عملیات رخ داد.");
                    return View(skill);
                case EditSkillResult.NotFoundSkill:
                    return NotFound("مهارت موردنظر یافت نشد");
            }

            return View(skill);
        }

        #endregion

        #endregion

        #endregion
    }
}
