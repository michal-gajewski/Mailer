using Database;
using Database.Tables;
using Domain.Commands;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
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
            var email = _context.Emails.Include(e => e.Recipients).FirstOrDefault(e => e.Id == command.EmailId);
            if (email == null)
                throw new EmailNotExistException();
            var newRecipient = new EmailRecipient { Address = command.Recipient };

            email.Recipients.Add(newRecipient);

            _context.SaveChanges();
        }
    }
}