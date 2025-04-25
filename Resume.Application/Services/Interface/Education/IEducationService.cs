using Resume.Domain.Dtos.Resume.Education;

namespace Resume.Application.Services.Interface.Education
{
    public interface IEducationService : IAsyncDisposable
    {
        Task<List<FilterEducationDto>> GetAllEducations();
        Task<CreateEducationResult> CreateEducation(CreateEducationDto command);
        Task<EditEducationDto> GetEducationForEdit(long id);
        Task<EditEducationResult> EditEducation(EditEducationDto education);
    }
}
