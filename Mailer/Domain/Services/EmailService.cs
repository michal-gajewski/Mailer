using Domain.Commands;
using Domain.Exceptions;
using Domain.Validators;
using System.Linq;

namespace Domain.Services
{
    public interface IEmailService
    {
        public CommandResult CreateEmail(string text, string title, string sender, string[] recipients);
    }

    public class EmailService : IEmailService
    {
        private readonly IAddEmailCommandHandler _addEmailCommandHandler;
        private readonly IAddRecipientCommandHandler _addRecipientCommandHandler;

        public EmailService(IAddEmailCommandHandler addEmailCommandHandler, IAddRecipientCommandHandler addRecipientCommandHandler)
        {
            _addEmailCommandHandler = addEmailCommandHandler;
            _addRecipientCommandHandler = addRecipientCommandHandler;
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
    }
}