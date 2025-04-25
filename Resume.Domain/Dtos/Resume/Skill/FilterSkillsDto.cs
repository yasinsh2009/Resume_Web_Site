using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.Resume.Skill
{
    public class FilterSkillsDto
    {
        public long Id { get; set; }
        public required string SkillTitle { get; set; }
        public required string SkillPercent { get; set; }
        public required string CreateDate { get; set; }
    }
}
