using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Resume.Domain.Dtos.Contact
{
    public class FilterContactMessageDto
    {
        public long Id { get; set; }
        public string? Location { get; set; }
        public string Fullname { get; set; }
        public string? EmailAddress { get; set; }
        public string Message { get; set; }
        public string CraeteDate { get; set; }
    }
}
