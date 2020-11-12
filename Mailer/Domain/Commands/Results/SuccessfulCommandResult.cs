namespace Domain.Commands
{
    public class SuccessfulCommandResult : CommandResult
    {
        public SuccessfulCommandResult()
        {
            IsSuccessful = true;
        }
    }
}