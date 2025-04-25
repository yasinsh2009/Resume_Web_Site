using MarketPlace.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.Contact;
using Resume.Domain.Dtos.Contact;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.Contact
{
    public class ContactService(IGenericRepository<Domain.Entities.Contact.Contact> contactRepository)
        : IContactService
    {
        #region Filter - Contact - Messages

        public async Task<List<FilterContactMessageDto>> GetAllContactMessages()
        {
            return await contactRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterContactMessageDto
                {
                    Id = x.Id,
                    Location = x.Location,
                    Fullname = x.Fullname,
                    EmailAddress = x.EmailAddress,
                    Message = x.Message,
                    CraeteDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        #endregion
        public async Task<CreateContactMessageResult> SendNewMessage(CreateContactMessageDto command)
        {
            if (command == null)
            {
                return CreateContactMessageResult.Failed("داده‌های ورودی نامعتبر است.");
            }

            try
            {
                // ایجاد پیغام تماس جدید
                var newContactMessage = new Domain.Entities.Contact.Contact(command.Location, command.Fullname, command.EmailAddress, command.Message);

                await contactRepository.AddEntity(newContactMessage);
                await contactRepository.SaveChanges();

                return CreateContactMessageResult.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating project: {ex.Message}");
                return CreateContactMessageResult.Failed("خطایی در ایجاد پروژه رخ داد.");
            }
        }

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (contactRepository != null)
            {
                await contactRepository.DisposeAsync();
                await contactRepository.DisposeAsync();
            }
        }

        #endregion
    }
}
