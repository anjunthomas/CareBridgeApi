namespace CareBridgeApi.Dtos
{
    public class EncounterResponseDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public int ProviderId { get; set; }

        public string ProviderName { get; set; } = string.Empty;

        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string EncounterType { get; set; } = string.Empty;

        public string ReasonForVisit { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
