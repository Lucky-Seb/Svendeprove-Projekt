namespace TaekwondoApp.Shared.DTO
{
    public class BrugerDTO
    {
        public Guid BrugerID { get; set; }
        public string Email { get; set; }
        public string Brugernavn { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Brugerkode { get; set; } // This should be hashed in your logic
        public string Bæltegrad { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public KlubDTO? Klub { get; set; }
        public string? Token { get; set; }  // Nullable since it will only be populated after successful authentication
    }
}
