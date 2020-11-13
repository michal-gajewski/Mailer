namespace Domain.Commands
{
    public class AddRecipientCommand
    {
        public AddRecipientCommand(long emailId, string recipient)
        {
            EmailId = emailId;
            Recipient = recipient;
        }

        public long EmailId { get; }
        public string Recipient { get; }
    }

    public interface IAddRecipientCommandHandler
    {
        void Handle(AddRecipientCommand command);
    }
}