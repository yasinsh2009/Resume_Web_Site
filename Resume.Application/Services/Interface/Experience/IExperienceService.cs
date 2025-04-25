using Resume.Domain.Dtos.Resume.Experience;

namespace Resume.Application.Services.Interface.Experience;

public interface IExperienceService : IAsyncDisposable
{
    Task<List<FilterExperiencesDto>> GetAllExperiences();
    Task<CreateExperienceResult> CreateExperience(CreateExperienceDto command);
    Task<EditExperienceDto> GetExperienceForEdit(long id);
    Task<EditExperienceResult> EditExperience(EditExperienceDto exp);
}