﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnglishLeagues.Models
{
    public class Match
    {
        public string Round { get; set; }

        public DateTime Date { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public Score Score;
    }
}
