using CareBridgeApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CareBridgeApi.Models;

namespace CareBridgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly CareBridgeDBContext _context;

        public PatientsController(CareBridgeDBContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            return Ok(patients);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if(patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient newPatient)
        {
            if(newPatient == null)
            {
                return BadRequest();
            }

            _context.Patients.Add(newPatient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPatientById), 
                new { 
                    id = newPatient.Id 
                }, newPatient
            );
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient updatedPatient)
        {
            var patient = await _context.Patients.FindAsync(id);
            if(patient == null)
            {
                return NotFound();
            }
            patient.FirstName = updatedPatient.FirstName;
            patient.LastName = updatedPatient.LastName;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Email = updatedPatient.Email;
            patient.PhoneNumber = updatedPatient.PhoneNumber;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var deletedPatient = await _context.Patients.FindAsync(id);
            if(deletedPatient == null)
            {
                return NotFound();
            }
            _context.Patients.Remove(deletedPatient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
