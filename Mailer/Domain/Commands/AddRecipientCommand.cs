namespace Domain.Commands
{
    public class AddRecipientCommand
    {
        public AddRecipientCommand(int emailId, string recipient)
        {
            EmailId = emailId;
            Recipient = recipient;
        }

        public int EmailId { get; }
        public string Recipient { get; }
    }

    public interface IAddRecipientCommandHandler
    {
        void Handle(AddRecipientCommand command);
    }
}