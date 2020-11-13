using System.Collections.Generic;
using Domain.Queries;
using Domain.Services;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailsController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IGetEmailsQueryHandler _getEmailsQueryHandler;

        public EmailsController(IEmailService emailService, IGetEmailsQueryHandler getEmailsQueryHandler)
        {
            _emailService = emailService;
            _getEmailsQueryHandler = getEmailsQueryHandler;

            emailService.CreateEmail("test", "Test", null, null);
        }

        [HttpGet]
        public IEnumerable<EmailDto> GetEmails()
        {
            return _emailService.GetEmails();
        }

        [HttpPost]
        public ActionResult Create(string text, string title, string sender, string[] recipients)
        {
            var result = _emailService.CreateEmail(text, title, sender, recipients);
            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.Message);
        }
    }
}