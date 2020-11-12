using Database;
using Database.Tables;
using Domain.Commands;
using Domain.Enumerations;
using System.Linq;

namespace DataAccess.CommandsHandlers
{
    public class AddEmailCommandHandler : IAddEmailCommandHandler
    {
        private readonly MailerDbContext _context;

        public AddEmailCommandHandler(MailerDbContext context)
        {
            this._context = context;
        }

        public void Handle(AddEmailCommand command)
        {
            _context.Add(new Email
            {
                Title = command.Title,
                Text = command.Text,
                Status = EmailStatus.PENDING,
                Recipients = command.Recipients.Select(r => new EmailRecipient { Address = r }).ToList()
            });

            _context.SaveChanges();
        }
    }
}