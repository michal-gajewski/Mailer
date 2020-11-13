using Infrastructure.DTOs.Enumerations;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class EmailDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Sender { get; set; }
        public EmailStatus Status { get; set; }
        public IEnumerable<EmailRecipientDto> Recipients { get; set; }
    }
}