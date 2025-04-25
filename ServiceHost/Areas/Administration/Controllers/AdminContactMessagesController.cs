using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface.Contact;
using Resume.Domain.Dtos.Contact;
using Resume.Domain.Dtos.Project;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class AdminContactMessagesController(IContactService contactService) : AdminBaseController
    {

        #region Get - All - Contact - Messages - Item

        [HttpGet("contact-messages-list")]
        public async Task<IActionResult> FilterContactMessages()
        {
            var contactMessage = await contactService.GetAllContactMessages();
            return View(contactMessage);
        }

        #endregion

    }
}
