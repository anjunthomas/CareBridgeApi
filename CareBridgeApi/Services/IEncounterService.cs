using CareBridgeApi.Dtos;

namespace CareBridgeApi.Services
{
    public interface IEncounterService
    {
        // task<list<encounter>> = an async operation that will eventually return a list of encounter responses
        Task<List<EncounterResponseDto>> GetEncountersAsync();
        Task<EncounterResponseDto?> GetEncounterByIdAsync(int id);

        Task<(EncounterResponseDto? Encounter, string? Error)> CreateEncounterAsync(
            CreateEncounterDto dto);

        Task<(EncounterResponseDto? Encounter, string? Error)> UpdateEncounterAsync(
            int id,
            UpdateEncounterDto dto);

        Task<bool> DeleteEncounterAsync(int id);
    }
}
