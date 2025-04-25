using MarketPlace.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.Experience;
using Resume.Domain.Dtos.Resume.Experience;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.Experience
{
    public class ExperienceService : IExperienceService
    {

        #region Fields

        private readonly IGenericRepository<Domain.Entities.Resume.Experience.Experience> _experienceRepository;

        #endregion

        #region Constructor

        public ExperienceService(IGenericRepository<Domain.Entities.Resume.Experience.Experience> experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        #endregion

        #region Filter - Experiences

        public async Task<List<FilterExperiencesDto>> GetAllExperiences()
        {
            return await _experienceRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterExperiencesDto
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    JobStartDate = x.JobStartDate,
                    JobEndDate = x.JobEndDate,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        #endregion

        #region Create - Experience

        public async Task<CreateExperienceResult> CreateExperience(CreateExperienceDto command)
        {
            var exp = new Domain.Entities.Resume.Experience.Experience
            {
                CompanyName = command.CompanyName,
                JobStartDate = command.JobStartDate,
                JobEndDate = command.JobEndDate,
                Description = command.Description,
            };

            _experienceRepository.AddEntity(exp);
            await _experienceRepository.SaveChanges();

            return CreateExperienceResult.Success;
        }

        #endregion

        #region Edit - Exprience

        public async Task<EditExperienceDto> GetExperienceForEdit(long id)
        {
            var exp = await _experienceRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (exp == null)
            {
                return new EditExperienceDto
                {
                    CompanyName = null,
                    JobStartDate = null,
                    JobEndDate = null,
                    Description = null
                };
            }

            return new EditExperienceDto
            {
                CompanyName = exp.CompanyName,
                JobStartDate = exp.JobStartDate,
                JobEndDate = exp.JobEndDate,
                Description = exp.Description,
            };
        }

        public async Task<EditExperienceResult> EditExperience(EditExperienceDto exp)
        {
            var existingExp = await _experienceRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == exp.Id);

            if (existingExp == null)
            {
                return EditExperienceResult.NotFoundExperience;
            }

            existingExp.CompanyName = exp.CompanyName;
            existingExp.JobStartDate = exp.JobStartDate;
            existingExp.JobEndDate = exp.JobEndDate;
            existingExp.Description = exp.Description;
            
            _experienceRepository.UpdateEntity(existingExp);
            await _experienceRepository.SaveChanges();

            return EditExperienceResult.Success;
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            // TODO release managed resources here
        }

        #endregion
    }
}
