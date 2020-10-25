using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLeagues.Models
{
    public class BestStatistic
    {
        public string Name { get; set; }
        public int Scored { get; set; }
        public int Missed { get; set; }
        public int Difference()
        {
            return Scored - Missed;
        }
    }
}
