using CareBridgeApi.Data;
using CareBridgeApi.Dtos;
using CareBridgeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CareBridgeApi.Services
{
    public class EncounterService : IEncounterService
    {
        private readonly CareBridgeDBContext _context;
        // ASP.NET core provides the DBcontext through dependency injection, and the constructor receives it
        public EncounterService(CareBridgeDBContext context)
        {
            _context = context;
        }

        private IQueryable<EncounterResponseDto> ProjectEncounters() // helper function to convert the Encounter into the encounterResponseDto
        {
            return _context.Encounters.Select(encounter => new EncounterResponseDto
            {
                Id = encounter.Id,
                PatientId = encounter.PatientId,
                PatientName = encounter.Patient!.FirstName + " " + encounter.Patient.LastName,
                ProviderId = encounter.ProviderId,
                ProviderName = encounter.Provider!.FirstName + " " + encounter.Provider.LastName,
                StartDateTime = encounter.StartDateTime,
                EndDateTime = encounter.EndDateTime,
                EncounterType = encounter.EncounterType,
                ReasonForVisit = encounter.ReasonForVisit,
                Status = encounter.Status
            });
        }

        public async Task<List<EncounterResponseDto>> GetEncountersAsync()
        {
            return await ProjectEncounters().ToListAsync();
        }

        public async Task<EncounterResponseDto?> GetEncounterByIdAsync(int id)
        {
            return await ProjectEncounters()
                .FirstOrDefaultAsync(encounter => encounter.Id == id);
        }

        public async Task<(EncounterResponseDto? Encounter, string? Error)> CreateEncounterAsync(
            CreateEncounterDto dto)
        {

            var patientExists = await _context.Patients
                .AnyAsync(patient => patient.Id == dto.PatientId);

            if (!patientExists)
            {
                return (null, "Patient does not exist.");
            }

            var providerExists = await _context.Providers
                .AnyAsync(provider => provider.Id == dto.ProviderId);

            if (!providerExists)
            {
                return (null, "Provider does not exist.");
            }

            if (dto.EndDateTime.HasValue &&
                dto.EndDateTime <= dto.StartDateTime)
            {
                return (null, "End time must be after the start time");
            }

            var encounter = new Encounter
            {
                PatientId = dto.PatientId,
                ProviderId = dto.ProviderId,
                StartDateTime = dto.StartDateTime,
                EndDateTime = dto.EndDateTime,
                EncounterType = dto.EncounterType,
                ReasonForVisit = dto.ReasonForVisit,
                Status = dto.Status
            };

            _context.Encounters.Add(encounter);
            await _context.SaveChangesAsync();

            return (await GetEncounterByIdAsync(encounter.Id), null);
        }

        public async Task<(EncounterResponseDto? Encounter, string? Error)> UpdateEncounterAsync(
            int id,
            UpdateEncounterDto dto)
        {
            var encounter = await _context.Encounters.FindAsync(id);

            if (encounter == null)
            {
                return (null, null);
            }

            var providerExists = await _context.Providers
                .AnyAsync(provider => provider.Id == dto.ProviderId);

            if (!providerExists)
            {
                return (null, "Provider does not exist.");
            }

            if (dto.EndDateTime.HasValue &&
                dto.EndDateTime <= dto.StartDateTime)
            {
                return (null, "End time must be after the start time.");
            }

            encounter.ProviderId = dto.ProviderId;
            encounter.StartDateTime = dto.StartDateTime;
            encounter.EndDateTime = dto.EndDateTime;
            encounter.EncounterType = dto.EncounterType;
            encounter.ReasonForVisit = dto.ReasonForVisit;
            encounter.Status = dto.Status;

            await _context.SaveChangesAsync();

            return (await GetEncounterByIdAsync(id), null);
        }
        public async Task<bool> DeleteEncounterAsync(int id)
        {
            var encounter = await _context.Encounters.FindAsync(id);

            if (encounter == null)
            {
                return false;
            }

            _context.Encounters.Remove(encounter);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
