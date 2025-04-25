namespace Resume.Domain.Dtos.Resume.Education
{
    public class EditEducationDto : CreateEducationDto
    {
        public long Id { get; set; }
    }
    public enum EditEducationResult
    {
        Success,
        Error,
        NotFoundEducation
    }
}
