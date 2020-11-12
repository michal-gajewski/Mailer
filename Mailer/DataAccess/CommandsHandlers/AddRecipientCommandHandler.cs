using Database;
using Database.Tables;
using Domain.Commands;
using Domain.Exceptions;
using System;
using System.Linq;

namespace DataAccess.CommandsHandlers
{
    public class AddRecipientCommandHandler : IAddRecipientCommandHandler
    {
        private readonly MailerDbContext _context;

        public AddRecipientCommandHandler(MailerDbContext context)
        {
            _context = context;
        }

        public void Handle(AddRecipientCommand command)
        {
            var email = _context.Emails.FirstOrDefault(e => e.Id == command.EmailId);
            if (email == null)
                throw new EmailNotExistException();

            _context.EmailRecipients.Add(new EmailRecipient { Email = email, Address = command.Recipient });
            _context.SaveChanges();
        }
    }
}