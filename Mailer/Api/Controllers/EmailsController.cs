using System.Collections.Generic;
using Domain.Queries;
using Domain.Services;
using Infrastructure.DTOs;
using Infrastructure.DTOs.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailsController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IEnumerable<EmailDto> GetEmails()
        {
            return _emailService.GetEmails();
        }

        [HttpGet]
        [Route("{emailId}")]
        public EmailDto GetEmails(long emailId)
        {
            return _emailService.GetEmail(emailId);
        }

        [HttpGet]
        [Route("{emailId}/status")]
        public EmailStatus GetEmailStatus(long emailId)
        {
            return _emailService.GetEmailStatus(emailId);
        }

        [HttpPost]
        public ActionResult Create(string text, string title, string sender, string[] recipients)
        {
            var result = _emailService.CreateEmail(text, title, sender, recipients);
            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("{emailId}/recipients")]
        public ActionResult AddRecipient(long emailId, [FromForm] string recipient)
        {
            var result = _emailService.AddRecipient(emailId, recipient);
            if (result.IsSuccessful)
                return Ok();

            return BadRequest(result.Message);
        }
    }
}