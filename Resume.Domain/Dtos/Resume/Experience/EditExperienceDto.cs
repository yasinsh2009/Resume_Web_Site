namespace Resume.Domain.Dtos.Resume.Experience
{
    public class EditExperienceDto : CreateExperienceDto
    {
        public long Id { get; set; }
    }
    public enum EditExperienceResult
    {
        Success,
        Error,
        NotFoundExperience
    }
}