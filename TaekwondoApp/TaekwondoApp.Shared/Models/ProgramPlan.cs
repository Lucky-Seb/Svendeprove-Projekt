namespace TaekwondoApp.Shared.Models
{
    public class ProgramPlan
    {
        public Guid ProgramID { get; set; }
        public string ProgramNavn { get; set; }
        public DateTime OprettelseDato { get; set; }
        public int Længde { get; set; }
        public string Beskrivelse { get; set; }

        public ICollection<KlubProgram> KlubProgrammer { get; set; }
        public ICollection<BrugerProgram> BrugerProgrammer { get; set; }
        public ICollection<Træning> Træninger { get; set; }
    }
}
