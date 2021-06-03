using DomainModel.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Email;

namespace WebApp.Controllers
{
    public class EmailController: Controller
    {
        private readonly IEmailNotifier _emailNotifier;

        public EmailController(IEmailNotifier emailNotifier)
        {
            _emailNotifier = emailNotifier;
        }

        [HttpPost("emails/test")]
        public IActionResult SendTestEmail([FromBody] SendTestEmailModel model)
        {
            _emailNotifier.SendTestEmail(model.EmailAddress);

            return Ok();
        }
    }
}