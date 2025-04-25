using Resume.Domain.Dtos.Project;

namespace Resume.Domain.Dtos.Contact
{
    public class EditContactMessageDto : CreateContactMessageDto
    {
        public long Id { get; set; }
    }
    public class EditContactMessageResult(bool isSuccess, string? message = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string? Message { get; } = message;

        public static EditContactMessageResult Success() => new EditContactMessageResult(true);
        public static EditContactMessageResult Failed(string message) => new EditContactMessageResult(false, message);
        public static EditContactMessageResult NotFound(string message) => new EditContactMessageResult(false, message);
    }
}
