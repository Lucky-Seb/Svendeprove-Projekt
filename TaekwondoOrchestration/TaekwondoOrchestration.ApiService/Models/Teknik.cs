﻿namespace TaekwondoOrchestration.ApiService.Models
{
    public class Teknik
    {
        public int TeknikID { get; set; }
        public string TeknikNavn { get; set; }
        public string TeknikBeskrivelse { get; set; }
        public string TeknikBillede { get; set; }
        public string TeknikVideo { get; set; }
        public string TeknikLyd { get; set; }
        public int PensumID { get; set; }
        public Pensum? Pensum { get; set; }
    }
}
