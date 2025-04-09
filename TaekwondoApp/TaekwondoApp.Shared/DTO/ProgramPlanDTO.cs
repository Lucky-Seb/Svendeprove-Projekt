namespace TaekwondoApp.Shared.DTO
{
    public class ProgramPlanDTO
    {
        public int ProgramID { get; set; }
        public string ProgramNavn { get; set; }
        public DateTime OprettelseDato { get; set; }
        public int Længde { get; set; }
        public string Beskrivelse { get; set; }
        public int BrugerID { get; set; }
        public int KlubID { get; set; }
        public List<TræningDTO> Træninger { get; set; }

    }
}
