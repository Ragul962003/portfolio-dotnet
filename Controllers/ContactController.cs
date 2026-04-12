using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;
using System.Net;
using System.Net.Mail;

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
                var emailUser = Environment.GetEnvironmentVariable("EMAIL_USER");
                var emailPass = Environment.GetEnvironmentVariable("EMAIL_PASS");

                if (string.IsNullOrEmpty(emailUser) || string.IsNullOrEmpty(emailPass))
                {
                    return BadRequest("Email environment variables not configured.");
                }

                var mail = new MailMessage();
                mail.From = new MailAddress(emailUser);
                mail.To.Add(emailUser);
                mail.Subject = "Portfolio Contact Message";

                mail.Body =
                    $"Name: {contact.Name}\n" +
                    $"Email: {contact.Email}\n" +
                    $"Message: {contact.Message}";

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(emailUser, emailPass),
                    EnableSsl = true,
                    Timeout = 20000
                };

                await smtp.SendMailAsync(mail);

                return Ok("Message sent successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Email sending failed: {ex.Message}");
            }
        }
    }
}