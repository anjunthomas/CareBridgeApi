using CareBridgeApi.Data;
using CareBridgeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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

            _context.Encounters.Add(newEncounter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEncounterById),
                new { id = newEncounter.Id },
                newEncounter
            );
        }
    }
}
