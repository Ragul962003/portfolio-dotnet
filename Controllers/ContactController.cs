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
                var mail = new MailMessage();

                mail.From = new MailAddress("ragulvincent09@gmail.com");
                mail.To.Add("ragulvincent09@gmail.com");

                mail.Subject = "New Contact Message From Portfolio";

                mail.Body = $"Name: {contact.Name}\n" +
                            $"Email: {contact.Email}\n" +
                            $"Message: {contact.Message}";

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
                        "ragulvincent09@gmail.com",
                        "tjpm prpi nwum pzwz"
                    ),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);

                return Ok(new
                {
                    message = "Thank you " + contact.Name + ", your message has been sent."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
