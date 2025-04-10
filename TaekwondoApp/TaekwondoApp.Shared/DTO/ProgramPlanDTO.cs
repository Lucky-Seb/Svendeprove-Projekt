namespace TaekwondoApp.Shared.DTO
{
    public class ProgramPlanDTO
    {
        public Guid ProgramID { get; set; }
        public string ProgramNavn { get; set; }
        public DateTime OprettelseDato { get; set; }
        public int Længde { get; set; }
        public string Beskrivelse { get; set; }
        public Guid BrugerID { get; set; }
        public Guid KlubID { get; set; }
        public List<TræningDTO> Træninger { get; set; }

    }
}
