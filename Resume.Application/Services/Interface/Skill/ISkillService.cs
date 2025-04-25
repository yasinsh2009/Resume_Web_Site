using Resume.Domain.Dtos.Resume.Skill;

namespace Resume.Application.Services.Interface.Skill;

public interface ISkillService : IAsyncDisposable
{
    Task<List<FilterSkillsDto>> GetAllSkills();
    Task<CreateSkillResult> CreateSkill(CreateSkillDto command);
    Task<EditSkillDto> GetSkillForEdit(long id);
    Task<EditSkillResult> EditSkill(EditSkillDto skill);
}