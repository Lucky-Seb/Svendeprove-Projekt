﻿using Microsoft.AspNetCore.Mvc;
using TaekwondoOrchestration.ApiService.Services;
using TaekwondoApp.Shared.DTO;


namespace TaekwondoOrchestration.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerProgramController : ControllerBase
    {
        private readonly BrugerProgramService _brugerProgramService;

        public BrugerProgramController(BrugerProgramService brugerProgramService)
        {
            _brugerProgramService = brugerProgramService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrugerProgramDTO>>> GetBrugerPrograms()
        {
            return Ok(await _brugerProgramService.GetAllBrugerProgramsAsync());
        }

        [HttpGet("{brugerId}/{programId}")]
        public async Task<ActionResult<BrugerProgramDTO>> GetBrugerProgram(Guid brugerId, Guid programId)
        {
            var brugerProgram = await _brugerProgramService.GetBrugerProgramByIdAsync(brugerId, programId);
            if (brugerProgram == null)
                return NotFound();
            return Ok(brugerProgram);
        }

        [HttpPost]
        public async Task<ActionResult<BrugerProgramDTO>> PostBrugerProgram(BrugerProgramDTO brugerProgramDto)
        {
            var createdBrugerProgram = await _brugerProgramService.CreateBrugerProgramAsync(brugerProgramDto);
            return CreatedAtAction(nameof(GetBrugerProgram), new { brugerId = createdBrugerProgram.BrugerID, programId = createdBrugerProgram.ProgramID }, createdBrugerProgram);
        }

        [HttpDelete("{brugerId}/{programId}")]
        public async Task<IActionResult> DeleteBrugerProgram(Guid brugerId, Guid programId)
        {
            var success = await _brugerProgramService.DeleteBrugerProgramAsync(brugerId, programId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
