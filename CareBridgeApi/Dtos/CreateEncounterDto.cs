namespace CareBridgeApi.Dtos
{
    public class CreateEncounterDto
    {
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }
  
        public string EncounterType { get; set; } = string.Empty;

        public string ReasonForVisit { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
