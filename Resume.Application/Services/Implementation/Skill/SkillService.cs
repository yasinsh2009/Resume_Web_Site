using System.Security.Cryptography.X509Certificates;
using MarketPlace.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.Skill;
using Resume.Domain.Dtos.Blog.ArticleCategory;
using Resume.Domain.Dtos.Resume.Skill;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.Skill
{
    public class SkillService : ISkillService
    {
        #region Fields

        private readonly IGenericRepository<Domain.Entities.Resume.Skill.Skill> _skillRepository;

        #endregion

        #region Constructor

        public SkillService(IGenericRepository<Domain.Entities.Resume.Skill.Skill> skillRepository)
        {
            _skillRepository = skillRepository;
        }

        #endregion

        #region Filter - Skills

        public async Task<List<FilterSkillsDto>> GetAllSkills()
        {
            return await _skillRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterSkillsDto
                {
                    Id = x.Id,
                    SkillTitle = x.SkillTitle,
                    SkillPercent = x.SkillPercent,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                })
                .ToListAsync();
        }

        #endregion

        #region Create - Skill

        public async Task<CreateSkillResult> CreateSkill(CreateSkillDto command)
        {
            var newSkill = new Domain.Entities.Resume.Skill.Skill
            {
                SkillTitle = command.SkillTitle,
                SkillPercent = command.SkillPercent
            };

            await _skillRepository.AddEntity(newSkill);
            await _skillRepository.SaveChanges();

            return CreateSkillResult.Success;
        }

        #endregion

        #region Edit - Skill

        public async Task<EditSkillDto> GetSkillForEdit(long id)
        {
            var skill = await _skillRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (skill == null)
            {
                return new EditSkillDto
                {
                    SkillTitle = null,
                    SkillPercent = null
                };
            }

            return new EditSkillDto
            {
                SkillTitle = skill.SkillTitle,
                SkillPercent = skill.SkillPercent,
            };
        }

        public async Task<EditSkillResult> EditSkill(EditSkillDto skill)
        {
            var existingSkill = await _skillRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == skill.Id);

            if (existingSkill == null)
            {
                return EditSkillResult.NotFoundSkill;
            }

            existingSkill.SkillTitle = skill.SkillTitle;
            existingSkill.SkillPercent = skill.SkillPercent;

            _skillRepository.UpdateEntity(existingSkill);
            await _skillRepository.SaveChanges();

            return EditSkillResult.Success;
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_skillRepository != null)
            {
                await _skillRepository.DisposeAsync();
            }
        }

        #endregion
    }
}
