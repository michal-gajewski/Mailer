namespace Domain.Commands
{
    public class AddEmailCommand
    {
        public AddEmailCommand(string text, string title, string sender, string[] recipients)
        {
            Text = text;
            Title = title;
            Sender = sender;
            Recipients = recipients;
        }

        public string Text { get; }
        public string Title { get; }
        public string Sender { get; }
        public string[] Recipients { get; }
    }

    public interface IAddEmailCommandHandler
    {
        public void Handle(AddEmailCommand command);
    }
}