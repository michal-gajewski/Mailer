using BackgroundWorker;
using Database.Tables;
using Domain.Commands;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tests
{
    [TestFixture]
    public class EmailSenderTests
    {
        private Mock<IBackgroundTaskQueue> _backgroundTaskQueueMock;
        private Mock<IMarkEmailAsSentCommandHandler> _markEmailAsSentCommandHandler;

        [SetUp]
        public void SetUp()
        {
            _backgroundTaskQueueMock = new Mock<IBackgroundTaskQueue>();
            _markEmailAsSentCommandHandler = new Mock<IMarkEmailAsSentCommandHandler>();
        }

        [Test]
        public void EmailForEveryRecipientShouldBeQueuedForSingleEmail()
        {
            var firstEmail = GetEmailWithSomeRecipients(2);

            IEmailSender emailSender = GetEmailSenderMockWithQueueMock();

            emailSender.Send(firstEmail);

            _backgroundTaskQueueMock.Verify((x) => x.QueueBackgroundWorkItem(It.IsAny<Func<CancellationToken, Task>>()), Times.Exactly(2));
        }

        private IEmailSender GetEmailSenderMockWithQueueMock()
        {
            return new EmailSender(_backgroundTaskQueueMock.Object, new Mock<IConfiguration>().Object, new Mock<ISmtpClient>().Object, _markEmailAsSentCommandHandler.Object);
        }

        private static Email GetEmailWithSomeRecipients(int recipientsCount)
        {
            var mock = new Mock<Email>();

            mock.Setup(e => e.Recipients)
            .Returns(new EmailRecipient[recipientsCount]);

            return mock.Object;
        }
    }
}