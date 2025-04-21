namespace TaekwondoApp.Shared.DTO
{
    public class BrugerDTO : SyncableEntityDTO
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

        // Related collections
        // List of related entities
        // Using ICollection<T> as a general collection type for flexibility
        public ICollection<KlubDTO> Klubber { get; set; } = new List<KlubDTO>();
        public ICollection<ProgramPlanDTO> Programmer { get; set; } = new List<ProgramPlanDTO>();
        public ICollection<QuizDTO> Quizzer { get; set; } = new List<QuizDTO>();
        public ICollection<ØvelseDTO> Øvelser { get; set; } = new List<ØvelseDTO>();
        public ICollection<BrugerKlubDTO> BrugerKlubber { get; set; } = new List<BrugerKlubDTO>();
    }
}
