using Database;
using Domain.Commands;
using Infrastructure.DTOs.Enumerations;
using System.Linq;

namespace DataAccess.CommandsHandlers
{
    public class MarkEmailAsSentCommandHandler : IMarkEmailAsSentCommandHandler
    {
        private readonly MailerDbContext _context;

        public MarkEmailAsSentCommandHandler(MailerDbContext context)
        {
            _context = context;
        }

        public void Handle(MarkEmailAsSentCommand command)
        {
            var email = _context.Emails.First(e => e.Id == command.EmailId);
            email.Status = EmailStatus.SENT;
            _context.SaveChanges();
        }
    }
}