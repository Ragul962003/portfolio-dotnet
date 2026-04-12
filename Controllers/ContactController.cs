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
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] contact contact)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress("ragulvincent09@gmail.com"),
                    Subject = "New Portfolio Message",
                    Body =
                        $"Name: {contact.Name}\n" +
                        $"Email: {contact.Email}\n" +
                        $"Message: {contact.Message}"
                };

                mail.To.Add("ragulvincent09@gmail.com");

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
         Environment.GetEnvironmentVariable("EMAIL_USER"),
         Environment.GetEnvironmentVariable("EMAIL_PASS")
     ),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);

                return Ok(new
                {
                    message = "Message sent successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Email sending failed: {ex.Message}");
            }
        }
    }
}
