using System.Threading.Tasks;

namespace Application
{
    public interface ISmtpClient
    {
        Task Send(string title, string text, string sender, string recipient);
    }

    public class SmtpClient : ISmtpClient
    {
        public Task Send(string title, string text, string sender, string recipient)
        {
            //here email should be send by smtp
            return Task.CompletedTask; ;
        }
    }
}