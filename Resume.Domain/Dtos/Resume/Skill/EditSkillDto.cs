namespace Resume.Domain.Dtos.Resume.Skill
{
    public class EditSkillDto : CreateSkillDto
    {
        public long Id { get; set; }
    }

    public enum EditSkillResult
    {
        Success,
        Error,
        NotFoundSkill
    }
}
