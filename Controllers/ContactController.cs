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

                var from = new EmailAddress("noreply@sendgrid.net", "Portfolio Contact");
                var to = new EmailAddress("ragulvincent09@gmail.com");

                var subject = "New Portfolio Contact Message";

                var plainTextContent =
                    $"Name: {contact.Name}\n" +
                    $"Email: {contact.Email}\n" +
                    $"Message: {contact.Message}";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
                msg.SetReplyTo(new EmailAddress("ragulvincent09@gmail.com", "Ragul V"));
                var response = await client.SendEmailAsync(msg);

                // AUTO RESPONSE

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
                    response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Auto reply to user
                    var autoSubject = "Thank you for contacting me";

                    var autoContent =
                        $"Hello {contact.Name},\n\n" +
                        $"Thank you for reaching out through my portfolio.\n\n" +
                        $"I have received your message and will get back to you as soon as possible.\n\n" +
                        $"Best regards,\nRagul V\nFull Stack Developer";

                    var autoMsg = MailHelper.CreateSingleEmail(
                        from,
                        new EmailAddress(contact.Email, contact.Name),
                        autoSubject,
                        autoContent,
                        null
                    );
                    autoMsg.SetReplyTo(new EmailAddress("ragulvincent09@gmail.com", "Ragul V"));
                    await client.SendEmailAsync(autoMsg);

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