﻿namespace TaekwondoOrchestration.ApiService.Models
{
    public class Pensum
    {
        public int PensumID { get; set; }
        public string PensumGrad { get; set; }

        // Navigation property
        public ICollection<Teori>? Teorier { get; set; }
        public ICollection<Teknik>? Teknikker { get; set; }
        public ICollection<Øvelse>? Øvelser { get; set; }
        public ICollection<Quiz>? Quizzer { get; set; }
    }
}
