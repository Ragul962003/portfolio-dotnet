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
                    return StatusCode(500, "Email configuration missing.");
                }

                var mail = new MailMessage
                {
                    From = new MailAddress(emailUser),
                    Subject = "Portfolio Contact Message",
                    Body = $"Name: {contact.Name}\nEmail: {contact.Email}\nMessage: {contact.Message}"
                };

                mail.To.Add(emailUser);

                var smtp = new SmtpClient("smtp.gmail.com", 465)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailUser, emailPass),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);

                return Ok(new { message = "Message sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}