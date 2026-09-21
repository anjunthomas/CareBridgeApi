namespace CareBridgeApi.Models
{
    public class Encounter
    {
        public int Id { get; set; }
        public int PatientId { get; set; } // foreign key for patient

        public Patient Patient { get; set; } = null!;

        public int ProviderId { get; set; } // fk for provider

        public Provider Provider { get; set; } = null!;

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string EncounterType { get; set; } = string.Empty;

        public string ReasonForVisit { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
