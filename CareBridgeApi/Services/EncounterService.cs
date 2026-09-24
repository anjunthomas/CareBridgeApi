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

        public async Task<List<Encounter>> GetEncountersAsync()
        {
            return await _context.Encounters
                .Include(encounter => encounter.Patient)
                .Include(encounter => encounter.Provider)
                .ToListAsync();
        }

        public async Task<Encounter?> GetEncounterByIdAsync(int id)
        {
            return await _context.Encounters
                .Include(encounter => encounter.Patient)
                .Include(encounter => encounter.Provider)
                .FirstOrDefaultAsync(encounter => encounter.Id == id);
        }

        public async Task<(Encounter? Encounter, string? Error)> CreateEncounterAsync(
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

            return (encounter, null);
        }

        public async Task<(Encounter? Encounter, string? Error)> UpdateEncounterAsync(
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

            return (encounter, null);
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
