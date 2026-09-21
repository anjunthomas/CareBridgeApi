namespace CareBridgeApi.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string FirstName{ get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public List<Encounter> Encounters { get; set; } = new();
    }
}
