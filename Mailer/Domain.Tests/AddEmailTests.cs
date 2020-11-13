using Domain.Commands;
using Domain.Queries;
using Domain.Services;
using Moq;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class AddEmailTests
    {
        private IEmailService _emailService;
        private Mock<IAddEmailCommandHandler> _addEmailCommandHandler;
        private Mock<IAddRecipientCommandHandler> _addRecipientCommandHandler;
        private Mock<IGetEmailsQueryHandler> _addGetEmailsQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _addEmailCommandHandler = new Mock<IAddEmailCommandHandler>();
            _addRecipientCommandHandler = new Mock<IAddRecipientCommandHandler>();
            _addGetEmailsQueryHandler = new Mock<IGetEmailsQueryHandler>();

            _emailService = new EmailService(_addEmailCommandHandler.Object, _addRecipientCommandHandler.Object, _addGetEmailsQueryHandler.Object);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldReturnValidationErrorWhenTitleIsNullOrEmpty(string title)
        {
            var result = _emailService.CreateEmail(title, null, null, null);

            Assert.IsFalse(result.IsSuccessful);
            Assert.That(!string.IsNullOrEmpty(result.Message));
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldReturnValidationErrorWhenTextIsNullOrEmpty(string text)
        {
            var result = _emailService.CreateEmail(null, text, null, null);

            Assert.IsFalse(result.IsSuccessful);
            Assert.That(!string.IsNullOrEmpty(result.Message));
        }

        [Test]
        public void ShouldReturnSuccesfulResultWhenParamsAreValid()
        {
            var result = _emailService.CreateEmail("title", "text", null, null);

            Assert.IsTrue(result.IsSuccessful);
            Assert.That(string.IsNullOrEmpty(result.Message));
        }

        [TestCase(null, "text")]
        [TestCase("", "text")]
        [TestCase("title", null)]
        [TestCase("", null)]
        public void CommandHandlerIsNotCalledWhenValidationFail(string title, string text)
        {
            var result = _emailService.CreateEmail(title, text, null, null);

            _addEmailCommandHandler.Verify((x) => x.Handle(It.IsAny<AddEmailCommand>()), Times.Never);
        }

        [TestCase("title", "text")]
        public void CommandHandlerIsCalledOnceWhenValidationPassed(string title, string text)
        {
            var result = _emailService.CreateEmail(title, text, null, null);

            _addEmailCommandHandler.Verify((x) => x.Handle(It.IsAny<AddEmailCommand>()), Times.Once);
        }
    }
}