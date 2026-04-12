using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

    namespace portFolio.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ResumeController : ControllerBase
        {
        [HttpGet("download")]
        public IActionResult DownloadResume()
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "resumes",
                "Ragul_Fullstack_Developer_resume.pdf"
            );

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Resume not found");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/pdf", "Ragul_Fullstack_Developer_resume.pdf");
        
    }
}
    }
