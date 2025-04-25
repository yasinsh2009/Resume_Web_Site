using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Contact;
using Resume.Domain.Dtos.Contact;

namespace ServiceHost.Controllers
{
    public class ContactMessageController(IContactService contactService) : Controller
    {

        #region Create - Contact - Message

        [HttpGet("contact-message-project")]
        public IActionResult SendNewMessage()
        {
            return View();
        }

        [HttpPost("contact-message-project"), ValidateAntiForgeryToken]
        public async Task<IActionResult> SendNewMessage(CreateContactMessageDto contactMessage)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "لطفاً تمامی فیلدها را به درستی پر کنید.";
                return View(contactMessage);
            }

            var result = await contactService.SendNewMessage(contactMessage);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "پروژه با موفقیت ایجاد شد.";
                return RedirectToAction("SendNewMessage", "ContactMessage");
            }
            else
            {
                TempData["ErrorMessage"] = result.Message ?? "خطایی در ایجاد پروژه رخ داد.";
                return View(contactMessage);
            }
        }


        #endregion
    }
}
