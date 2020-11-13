using Domain.Commands;
using Domain.Exceptions;
using Domain.Queries;
using Domain.Validators;
using Infrastructure.DTOs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public interface IEmailService
    {
        CommandResult CreateEmail(string text, string title, string sender, string[] recipients);

        CommandResult AddRecipient(int emailId, string recipient);

        IEnumerable<EmailDto> GetEmails();
    }

    public class EmailService : IEmailService
    {
        private readonly IAddEmailCommandHandler _addEmailCommandHandler;
        private readonly IAddRecipientCommandHandler _addRecipientCommandHandler;
        private readonly IGetEmailsQueryHandler _getEmailsQueryHandler;

        public EmailService(IAddEmailCommandHandler addEmailCommandHandler,
            IAddRecipientCommandHandler addRecipientCommandHandler,
            IGetEmailsQueryHandler getEmailsQueryHandler)
        {
            _addEmailCommandHandler = addEmailCommandHandler;
            _addRecipientCommandHandler = addRecipientCommandHandler;
            _getEmailsQueryHandler = getEmailsQueryHandler;
        }

        public CommandResult CreateEmail(string text, string title, string sender, string[] recipients)
        {
            var validator = new AddEmailCommandValidator();
            var command = new AddEmailCommand(text, title, sender, recipients);

            var result = validator.Validate(command);

            if (result.IsValid)
            {
                _addEmailCommandHandler.Handle(command);
                return new SuccessfulCommandResult();
            }
            else
            {
                return new CommandFailedResult(result.Errors);
            }
        }

        public CommandResult AddRecipient(int emailId, string recipient)
        {
            try
            {
                _addRecipientCommandHandler.Handle(new AddRecipientCommand(emailId, recipient));
                return new SuccessfulCommandResult();
            }
            catch (EmailNotExistException)
            {
                return new CommandFailedResult("Email does not exists");
            }
        }

        public IEnumerable<EmailDto> GetEmails()
        {
            var emails = _getEmailsQueryHandler.Handle(new GetEmailsQuery());

            return emails.Select(
                e => new EmailDto
                {
                    Id = e.Id,
                    Sender = e.Sender,
                    Status = e.Status,
                    Text = e.Text,
                    Title = e.Title,
                    Recipients = e.Recipients != null ? e.Recipients.Select(r => new EmailRecipientDto { Id = r.Id, Address = r.Address }) : null
                });
        }
    }
}