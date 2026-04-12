using Microsoft.AspNetCore.Http;
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
        private static List<contact> contacts = new List<contact>();

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] contact contact)
        {
            try
            {
                var emailUser = Environment.GetEnvironmentVariable("EMAIL_USER");
                var emailPass = Environment.GetEnvironmentVariable("EMAIL_PASS");

                var mail = new MailMessage
                {
                    From = new MailAddress(emailUser),
                    Subject = "Portfolio Contact Message",
                    Body = $"Name: {contact.Name}\nEmail: {contact.Email}\nMessage: {contact.Message}"
                };

                mail.To.Add(emailUser);

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(emailUser, emailPass),
                    EnableSsl = true
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
