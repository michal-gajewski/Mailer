using BackgroundWorker;
using Database.Tables;
using Domain.Commands;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface IEmailSender
    {
        void Send(Email email);

        void Send(IEnumerable<Email> emails);
    }

    public class EmailSender : IEmailSender
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IConfiguration _configuration;
        private readonly ISmtpClient _smtpClient;
        private readonly IMarkEmailAsSentCommandHandler _markEmailAsSentCommandHandler;

        public EmailSender(IBackgroundTaskQueue taskQueue,
            IConfiguration configuration,
            ISmtpClient smtpClient,
            IMarkEmailAsSentCommandHandler markEmailAsSentCommandHandler)
        {
            _taskQueue = taskQueue;
            _configuration = configuration;
            _smtpClient = smtpClient;
            _markEmailAsSentCommandHandler = markEmailAsSentCommandHandler;
        }

        public void Send(IEnumerable<Email> emails)
        {
            foreach (var email in emails)
            {
                Send(email);
            }
        }

        public void Send(Email email)
        {
            var sender = GetSender(email);
            foreach (var recipient in email.Recipients)
            {
                _taskQueue.QueueBackgroundWorkItem(async token =>
                {
                    await Task.WhenAll(_smtpClient.Send(email.Title, email.Text, sender, recipient.Address));
                    _markEmailAsSentCommandHandler.Handle(new MarkEmailAsSentCommand(email.Id));
                });
            }
        }

        private string GetSender(Email email)
        {
            return email.Sender ?? _configuration["defaultSender"];
        }
    }
}