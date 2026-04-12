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
        public IActionResult SendMessage([FromBody] contact contact)
        {
            try
            {

                var mail = new MailMessage();
                mail.From = new MailAddress($"{contact.Email}");
                mail.To.Add("ragulvincent09@gmail.com");

                mail.Subject = $"{contact.Message}";

                mail.Body = $"Name: {contact.Name}\n" +
                            $"Email: {contact.Email}\n" +
                            $"Message: {contact.Message}";

                var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("ragulvincent09@gmail.com", "tjpm prpi nwum pzwz");
                smtp.EnableSsl = true;

                smtp.Send(mail);

                return Ok(new
                {
                    message = "Thank You, " + contact.Name + ", Your Message Sent Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    }
    }
}
