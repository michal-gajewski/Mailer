using Database;
using Database.Tables;
using Domain.Commands;
using Infrastructure.DTOs.Enumerations;
using System.Linq;

namespace DataAccess.CommandsHandlers
{
    public class AddEmailCommandHandler : IAddEmailCommandHandler
    {
        private readonly MailerDbContext _context;

        public AddEmailCommandHandler(MailerDbContext context)
        {
            _context = context;
        }

        public void Handle(AddEmailCommand command)
        {
            _context.Add(new Email
            {
                Title = command.Title,
                Text = command.Text,
                Status = EmailStatus.PENDING,
                Sender = command.Sender,
                Recipients = command.Recipients.Select(r => new EmailRecipient { Address = r }).ToList()
            });

            _context.SaveChanges();
        }
    }
}