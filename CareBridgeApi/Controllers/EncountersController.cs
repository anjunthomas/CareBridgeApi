using CareBridgeApi.Data;
using CareBridgeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareBridgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncountersController : ControllerBase
    {
        private readonly CareBridgeDBContext _context;

        public EncountersController(CareBridgeDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Encounter>>> GetEncounters()
        {
            var encounters = await _context.Encounters
                .Include(encounter => encounter.Patient) // also want to retrieve the associated Patient and Provider objects when getting the encounters
                .Include(encounter => encounter.Provider)
                .ToListAsync();

            return Ok(encounters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Encounter>> GetEncounterById(int id)
        {
            var encounter = await _context.Encounters
                .Include(encounter => encounter.Patient)
                .Include(encounter => encounter.Provider)
                .FirstOrDefaultAsync(encounter => encounter.Id == id);

            if (encounter == null)
            {
                return NotFound();
            }

            return Ok(encounter);
        }

        [HttpPost]
        public async Task<ActionResult<Encounter>> CreateEncounter(Encounter newEncounter)
        {
            var patientExists = await _context.Patients
                .AnyAsync(patient => patient.Id == newEncounter.PatientId);

            if (!patientExists)
            {
                return BadRequest("Patient does not exist.");
            }

            var providerExists = await _context.Providers
                .AnyAsync(provider => provider.Id == newEncounter.ProviderId);

            if (!providerExists)
            {
                return BadRequest("Provider does not exist.");
            }

            if(newEncounter.EndDateTime.HasValue && 
                newEncounter.EndDateTime <= newEncounter.StartDateTime)
            {
                return BadRequest("End time should be after the start time.");
            }

            _context.Encounters.Add(newEncounter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEncounterById),
                new { id = newEncounter.Id },
                newEncounter
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Encounter>> UpdateEncounter(int id, Encounter updatedEncounter)
        {
            var encounter = await _context.Encounters.FindAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }

            var patientExists = await _context.Patients
                .AnyAsync(patient => patient.Id == updatedEncounter.PatientId);

            if (!patientExists)
            {
                return BadRequest("This patient doesn't exist");
            }

            var providerExists = await _context.Providers
                .AnyAsync(provider => provider.Id == updatedEncounter.ProviderId);

            if (!providerExists)
            {
                return BadRequest("This provider doesn't exist");
            }

            if(updatedEncounter.EndDateTime.HasValue && 
                updatedEncounter.EndDateTime <= updatedEncounter.StartDateTime)
            {
                return BadRequest("End time must be after the start time.");
            }

            encounter.PatientId = updatedEncounter.PatientId;
            encounter.ProviderId = updatedEncounter.ProviderId;
            encounter.StartDateTime = updatedEncounter.StartDateTime;
            encounter.EndDateTime = updatedEncounter.EndDateTime;
            encounter.EncounterType = updatedEncounter.EncounterType;
            encounter.ReasonForVisit = updatedEncounter.ReasonForVisit;
            encounter.Status = updatedEncounter.Status;

            await _context.SaveChangesAsync();
            return Ok(encounter);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncounter(int id)
        {
            var encounter = await _context.Encounters.FindAsync(id);
            if (encounter == null) 
            {
                return NotFound();
            }
            _context.Encounters.Remove(encounter);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
