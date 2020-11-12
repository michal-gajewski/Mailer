using Domain.Enumerations;
using System.Collections.Generic;

namespace Database.Tables
{
    public class Email
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Sender { get; set; }
        public EmailStatus Status { get; set; }
        public ICollection<EmailRecipient> Recipients { get; set; }
    }
}