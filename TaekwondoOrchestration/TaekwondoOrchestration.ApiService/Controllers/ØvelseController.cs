using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.NotificationHubs;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ØvelseController : ApiBaseController
    {
        private readonly IØvelseService _øvelseService;
        private readonly IHubContext<ØvelseHub> _hubContext;  // If you want real-time notifications

        public ØvelseController(IØvelseService øvelseService, IHubContext<ØvelseHub> hubContext)
        {
            _øvelseService = øvelseService;
            _hubContext = hubContext;
        }

        // GET: api/Øvelse
        [HttpGet]
        public async Task<IActionResult> GetØvelser()
        {
            var result = await _øvelseService.GetAllØvelserAsync();
            return result.ToApiResponse();
        }

        [HttpGet("own")]
        public async Task<IActionResult> GetØvelser([FromQuery] Guid? brugerId = null, [FromQuery] string klubIds = "")
        {
            var klubIdList = klubIds?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(Guid.Parse)
                .ToList() ?? new List<Guid>();

            var result = await _øvelseService.GetFilteredØvelserAsync(brugerId, klubIdList);
            return result.ToApiResponse();
        }
        // GET: api/Øvelse/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetØvelse(Guid id)
        {
            var result = await _øvelseService.GetØvelseByIdAsync(id);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/sværhed/{sværhed}
        [HttpGet("sværhed/{sværhed}")]
        public async Task<IActionResult> GetØvelserBySværhed(string sværhed)
        {
            var result = await _øvelseService.GetØvelserBySværhedAsync(sværhed);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/bruger/{brugerId}
        [HttpGet("bruger/{brugerId}")]
        public async Task<IActionResult> GetØvelserByBruger(Guid brugerId)
        {
            var result = await _øvelseService.GetØvelserByBrugerAsync(brugerId);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/klub/{klubId}
        [HttpGet("klub/{klubId}")]
        public async Task<IActionResult> GetØvelserByKlub(Guid klubId)
        {
            var result = await _øvelseService.GetØvelserByKlubAsync(klubId);
            return result.ToApiResponse();
        }

        // GET: api/Øvelse/navn/{navn}
        [HttpGet("navn/{navn}")]
        public async Task<IActionResult> GetØvelserByNavn(string navn)
        {
            var result = await _øvelseService.GetØvelserByNavnAsync(navn);
            return result.ToApiResponse();
        }

        // POST: api/Øvelse
        [HttpPost]
        public async Task<IActionResult> PostØvelse(ØvelseDTO øvelseDto)
        {
            var result = await _øvelseService.CreateØvelseAsync(øvelseDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ØvelseUpdated");

            return result.ToApiResponse();
        }
        // PUT: api/Øvelse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutØvelse(Guid id, [FromBody] ØvelseDTO updatedØvelseDto)
        {
            if (id != updatedØvelseDto.ØvelseID)
            {
                return BadRequest("Exercise ID mismatch.");
            }

            // Call the service to update the exercise
            var result = await _øvelseService.UpdateØvelseAsync(id, updatedØvelseDto);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ØvelseUpdated");

            return result.ToApiResponse();
        }
        // DELETE: api/Øvelse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteØvelse(Guid id)
        {
            var result = await _øvelseService.DeleteØvelseAsync(id);

            // Optionally, trigger notifications if required
            if (result.Success)
                await _hubContext.Clients.All.SendAsync("ØvelseDeleted");

            return result.ToApiResponse();
        }
        //// POST: api/Øvelse/upload/øvelse/{øvelseNavn}/{fileName}
        //[HttpPost("upload/øvelse/{øvelseNavn}/{fileName}")]
        //public async Task<IActionResult> UploadFile(string øvelseNavn, string fileName)
        //{
        //    try
        //    {
        //        // Ensure that the request has a Content-Type header indicating multipart/form-data
        //        if (!Request.ContentType.Contains("multipart/form-data"))
        //        {
        //            return BadRequest("Content-Type must be multipart/form-data.");
        //        }

        //        // Check if the file is included in the request
        //        var file = Request.Form.Files.FirstOrDefault();
        //        if (file == null)
        //        {
        //            return BadRequest("No file uploaded.");
        //        }

        //        // Validate file size or type if necessary (optional, based on your use case)
        //        if (file.Length == 0)
        //        {
        //            return BadRequest("Uploaded file is empty.");
        //        }

        //        // Optional: Check file extension (for example, allow only images or specific file types)
        //        string[] allowedExtensions = { ".jpg", ".png", ".txt", ".pdf" }; // Modify as needed
        //        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        //        if (!allowedExtensions.Contains(fileExtension))
        //        {
        //            return BadRequest($"Invalid file type. Allowed types are: {string.Join(", ", allowedExtensions)}.");
        //        }

        //        // Create folder path
        //        var folderPath = Path.Combine("C:\\inetpub\\wwwroot\\øvelse", øvelseNavn);

        //        // Ensure folder exists
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }

        //        // Set file path
        //        var filePath = Path.Combine(folderPath, fileName);

        //        // Save the file to the server
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        // Return success response with a message
        //        return Ok(new { Message = "File uploaded successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception (logging can be done here)
        //        // Return a generic error message with the exception details for debugging
        //        return StatusCode(500, new { Message = "An error occurred while uploading the file.", Error = ex.Message });
        //    }
        //}
    }
}
