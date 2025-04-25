using Resume.Domain.Dtos.Contact;

namespace Resume.Application.Services.Interface.Contact;

public interface IContactService : IAsyncDisposable
{
    Task<List<FilterContactMessageDto>> GetAllContactMessages();
    Task<CreateContactMessageResult> SendNewMessage(CreateContactMessageDto command);
}