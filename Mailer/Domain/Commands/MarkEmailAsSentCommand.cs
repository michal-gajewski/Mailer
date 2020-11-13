namespace Domain.Commands
{
    public class MarkEmailAsSentCommand
    {
        public MarkEmailAsSentCommand(long emailId)
        {
            EmailId = emailId;
        }

        public long EmailId { get; }
    }

    public interface IMarkEmailAsSentCommandHandler
    {
        void Handle(MarkEmailAsSentCommand command);
    }
}