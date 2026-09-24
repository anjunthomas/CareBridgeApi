using CareBridgeApi.Models;
using CareBridgeApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using CareBridgeApi.Services;

namespace CareBridgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncountersController : ControllerBase
    {
 
        private readonly IEncounterService _encounterService;

        public EncountersController(
            IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Encounter>>> GetEncounters()
        {
            var encounters = await _encounterService.GetEncountersAsync();

            return Ok(encounters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Encounter>> GetEncounterById(int id)
        {
            var encounter = await _encounterService.GetEncounterByIdAsync(id);

            if (encounter == null)
            {
                return NotFound();
            }

            return Ok(encounter);
        }

        [HttpPost]
        public async Task<ActionResult<Encounter>> CreateEncounter(
            CreateEncounterDto dto)
        {

            var result = await _encounterService.CreateEncounterAsync(dto);
            
            if(result.Error != null)
            {
                return BadRequest(result.Error);
            }

            var encounter = result.Encounter!;

            return CreatedAtAction(
                nameof(GetEncounterById),
                new { id = encounter.Id },
                encounter
            );
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Encounter>> UpdateEncounter(
            int id,
            UpdateEncounterDto dto)
        {
            var result = await _encounterService.UpdateEncounterAsync(id, dto);

            if (result.Encounter == null && result.Error == null)
            {
                return NotFound();
            }

            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Encounter);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncounter(int id)
        {
            var deleted = await _encounterService.DeleteEncounterAsync(id);
            
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
