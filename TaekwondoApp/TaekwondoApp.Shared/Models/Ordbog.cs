﻿using SQLite;

namespace TaekwondoApp.Shared.Models
{
    public class Ordbog : SyncableEntity
    {
        [PrimaryKey] public Guid OrdbogId { get; set; }
        public string DanskOrd { get; set; }
        public string KoranskOrd { get; set; }
        public string Beskrivelse { get; set; }
        public string BilledeLink { get; set; }
        public string LydLink { get; set; }
        public string VideoLink { get; set; }

    }
}