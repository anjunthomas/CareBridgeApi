using CareBridgeApi.Dtos;
using CareBridgeApi.Models;

namespace CareBridgeApi.Services
{
    public interface IEncounterService
    {
        // task<list<encounter>> = an async operation that will eventually return a list of encounters
        Task<List<Encounter>> GetEncountersAsync();
        Task<Encounter?> GetEncounterByIdAsync(int id);

        Task<(Encounter? Encounter, string? Error)> CreateEncounterAsync(
            CreateEncounterDto dto);

        Task<(Encounter? Encounter, string? Error)> UpdateEncounterAsync(
            int id,
            UpdateEncounterDto dto);

        Task<bool> DeleteEncounterAsync(int id);
    }
}
