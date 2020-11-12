using DataAccess.Commands;
using Domain.Commands;
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

        public EmailService(IAddEmailCommandHandler addEmailCommandHandler)
        {
            _addEmailCommandHandler = addEmailCommandHandler;
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
    }
}