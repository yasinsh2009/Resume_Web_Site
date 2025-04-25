namespace Resume.Domain.Dtos.Project.Project
{
    public class EditProjectDto : CreateProjectDto
    {
        public long Id { get; set; }
    }
    public class EditProjectResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static EditProjectResult Success() => new EditProjectResult(true);
        public static EditProjectResult Failed(string message) => new EditProjectResult(false, message);
        public static EditProjectResult NotFound(string message) => new EditProjectResult(false, message);
    }
}
