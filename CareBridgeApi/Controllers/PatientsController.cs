using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CareBridgeApi.Models;

namespace CareBridgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        static private List<Patient> patients = new List<Patient>
        {
            new Patient
            {
                Id = 1,
                FirstName = "Jolyne",
                LastName = "Kujoh",
                DateOfBirth = new DateOnly(1990, 1, 1),
                Email = "jolyne@gmail.com",
                PhoneNumber = "123-456-7890",
            },
            new  Patient
            {
                Id = 2,
                FirstName = "Jojo",
                LastName = "Joestar",
                DateOfBirth = new DateOnly(1983, 12, 1),
                Email = "jojo@gmail.com",
                PhoneNumber = null,
            },
            new Patient
            {
                Id = 3,
                FirstName = "Josuke",
                LastName = "Higashikata",
                DateOfBirth = new DateOnly(1988, 2, 1),
                Email = "josuke@gmail.com",
                PhoneNumber = null,
            },
            new Patient
            {
                Id = 4,
                FirstName = "Joseph",
                LastName = "Joestar",
                DateOfBirth = new DateOnly(1992, 4, 1),
                Email = "joseph@gmail.com",
                PhoneNumber = null,
            },
            new Patient
            {
                Id = 5,
                FirstName = "Jotaro",
                LastName = "Kujo",
                DateOfBirth = new DateOnly(1993, 7, 1),
                Email = "jotarojoseph@gmail.com",
                PhoneNumber = null, // phone number can be nullable, so use null instead of an empty string
            },
        };
        [HttpGet]
        public ActionResult<List<Patient>> GetPatients()
        {
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientById(int id)
        {
            var patient = patients.FirstOrDefault(x => x.Id == id);
            if(patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public ActionResult<Patient> CreatePatient(Patient newPatient)
        {
            if(newPatient == null)
            {
                return BadRequest();
            }
            patients.Add(newPatient);
            return CreatedAtAction(
                nameof(GetPatientById), 
                new { 
                    id = newPatient.Id 
                }, newPatient
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, Patient updatedPatient)
        {
            var patient = patients.FirstOrDefault(x => x.Id == id);
            if(patient == null)
            {
                return NotFound();
            }
            patient.FirstName = updatedPatient.FirstName;
            patient.LastName = updatedPatient.LastName;
            patient.DateOfBirth = updatedPatient.DateOfBirth;
            patient.Email = updatedPatient.Email;
            patient.PhoneNumber = updatedPatient.PhoneNumber;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var deletedPatient = patients.FirstOrDefault(x => x.Id == id);
            if(deletedPatient == null)
            {
                return NotFound();
            }
            patients.Remove(deletedPatient);
            return NoContent();
        }
    }
}
