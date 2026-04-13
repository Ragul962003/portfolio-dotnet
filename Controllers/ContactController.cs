using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace portFolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

[HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] contact contact)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

                if (string.IsNullOrEmpty(apiKey))
                {
                    return StatusCode(500, "SendGrid API key not configured.");
                }

                var client = new SendGridClient(apiKey);

                var from = new EmailAddress("ragulvincent09@gmail.com", "Portfolio Contact");
                var to = new EmailAddress("ragulvincent09@gmail.com");

                var subject = "New Portfolio Contact Message";

                var plainTextContent =
                    $"Name: {contact.Name}\n" +
                    $"Email: {contact.Email}\n" +
                    $"Message: {contact.Message}";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
                    response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok("Message sent successfully");
                }

                return StatusCode((int)response.StatusCode, "Failed to send email.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Email sending failed: {ex.Message}");
            }
        }
    }
}