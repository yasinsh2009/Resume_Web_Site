using MarketPlace.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.Education;
using Resume.Domain.Dtos.Resume.Education;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.Education
{
    public class EducationService : IEducationService
    {

        #region Fields

        private readonly IGenericRepository<Domain.Entities.Resume.Education.Education> _educationRepository;

        #endregion

        #region Constructor

        public EducationService(IGenericRepository<Domain.Entities.Resume.Education.Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        #endregion

        #region Filter - Educations

        public async Task<List<FilterEducationDto>> GetAllEducations()
        {
            return await _educationRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterEducationDto
                {
                    Id = x.Id,
                    UnivercityName = x.UniversityName,
                    EducatioStartDate = x.EducationStartDate,
                    EducationEndDate = x.EducationEndDate,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id).ToListAsync();
        }

        #endregion

        #region Create - Education

        public async Task<CreateEducationResult> CreateEducation(CreateEducationDto command)
        {
            var newEducation = new Domain.Entities.Resume.Education.Education
            {
                UniversityName = command.UnivercityName,
                EducationStartDate = command.EducatioStartDate,
                EducationEndDate = command.EducationEndDate,
                Description = command.Description
            };

            _educationRepository.AddEntity(newEducation);
            await _educationRepository.SaveChanges();

            return CreateEducationResult.Success;
        }

        #endregion

        #region Edit - Education

        public async Task<EditEducationDto> GetEducationForEdit(long id)
        {
            var education = await _educationRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (education == null)
            {
                return new EditEducationDto
                {
                    UnivercityName = null,
                    EducatioStartDate = null,
                    EducationEndDate = null,
                    Description = null,
                };
            }

            return new EditEducationDto
            {
                UnivercityName = education.UniversityName,
                EducatioStartDate = education.EducationStartDate,
                EducationEndDate = education.EducationEndDate,
                Description = education.Description
            };
        }

        public async Task<EditEducationResult> EditEducation(EditEducationDto education)
        {
            var existingEducation = await _educationRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == education.Id);

            if (existingEducation == null)
            {
                return EditEducationResult.NotFoundEducation;
            }

            existingEducation.UniversityName = education.UnivercityName;
            existingEducation.EducationStartDate = education.EducatioStartDate;
            existingEducation.EducationEndDate = education.EducationEndDate;
            existingEducation.Description = education.Description;

            _educationRepository.UpdateEntity(existingEducation);
            await _educationRepository.SaveChanges();   

            return EditEducationResult.Success;
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_educationRepository != null)
            {
                await _educationRepository.DisposeAsync();
            }
        }

        #endregion
    }
}
