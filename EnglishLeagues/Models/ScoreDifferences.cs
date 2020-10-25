using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLeagues.Models
{
    public class ScoreDifferences
    {
        public int ScoreDiff { get; set; }
        public int Score { get; set; }
        public void addScore(int _score)
        {
            Score += _score;
        }
        public void addScoreDiff(int _scoreDiff)
        {
            ScoreDiff += _scoreDiff;
        }
    }
}
