using EnglishLeagues.Models;
using EnglishLeagues.Ries;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLeagues.Services
{
    public class LeagueService
    {
        private string en1 = Path.GetFullPath("Data/en.1.json");
        private string en2 = Path.GetFullPath("Data/en.2.json");
        private string en3 = Path.GetFullPath("Data/en.3.json");
        private LeaguesRepository _repository;
        public LeagueService(LeaguesRepository repository)
        {
            _repository = repository;
        }
        
        private Dictionary<string, int> GetBestAttackTeamDict(League league)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            foreach(var x in league.Matches)
            {
                if(x.Score!=null)
                {
                    if (!temp.ContainsKey(x.Team1))
                    {
                        temp.Add(x.Team1, x.Score.ft[0]);
                    }
                    else
                    {
                        temp[x.Team1] += x.Score.ft[0];
                    }
                    if (!temp.ContainsKey(x.Team2))
                    {
                        temp.Add(x.Team2, x.Score.ft[1]);
                    }
                    else
                    {
                        temp[x.Team2] += x.Score.ft[1];
                    }
                }
                
            }
            return temp;
        }
        private Dictionary<string, int> GetDayDict(League league)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            foreach(var x in league.Matches)
            {
                if (x.Score != null)
                {
                    if (!temp.ContainsKey(x.Round))
                    {
                        temp.Add(x.Round, x.Score.ft[0] + x.Score.ft[1]);
                    }
                    else
                    {
                        temp[x.Round] += x.Score.ft[0] + x.Score.ft[1];
                    }
                }
                    
            }
            return temp;
        }

        private Dictionary<string, int> GetBestDefenceTeamDict(League league)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            foreach (var x in league.Matches)
            {
                if (x.Score != null)
                {
                    if (!temp.ContainsKey(x.Team1))
                    {
                        temp.Add(x.Team1, x.Score.ft[1]);
                    }
                    else
                    {
                        temp[x.Team1] += x.Score.ft[1];
                    }
                    if (!temp.ContainsKey(x.Team2))
                    {
                        temp.Add(x.Team2, x.Score.ft[0]);
                    }
                    else
                    {
                        temp[x.Team2] += x.Score.ft[0];
                    }
                }

            }
            return temp;
        }

        public List<BestTeam> GetBestAttackTeams()
        {
            List<BestTeam> bestTeams = new List<BestTeam>();
            League league1 = _repository.GetLeagues(en1);
            League league2 = _repository.GetLeagues(en2);
            League league3 = _repository.GetLeagues(en3);
            Dictionary<string, int> league1Dict = GetBestAttackTeamDict(league1);
            Dictionary<string, int> league2Dict = GetBestAttackTeamDict(league2);
            Dictionary<string, int> league3Dict = GetBestAttackTeamDict(league3);
            var league1MaxValue = league1Dict.FirstOrDefault(x => x.Value == league1Dict.Values.Max());
            var league2MaxValue = league2Dict.FirstOrDefault(x => x.Value == league2Dict.Values.Max());
            var league3MaxValue = league3Dict.FirstOrDefault(x => x.Value == league3Dict.Values.Max());
            bestTeams.Add(new BestTeam { Team = league1MaxValue.Key, Score = league1MaxValue.Value });
            bestTeams.Add(new BestTeam { Team = league2MaxValue.Key, Score = league2MaxValue.Value });
            bestTeams.Add(new BestTeam { Team = league3MaxValue.Key, Score = league3MaxValue.Value });
            return bestTeams; 
        }

        public List<BestDefTeam> GetBestDefTeams()
        {
            List<BestDefTeam> bestDefTeams = new List<BestDefTeam>();
            League league1 = _repository.GetLeagues(en1);
            League league2 = _repository.GetLeagues(en2);
            League league3 = _repository.GetLeagues(en3);
            Dictionary<string, int> league1Dict = GetBestDefenceTeamDict(league1);
            Dictionary<string, int> league2Dict = GetBestDefenceTeamDict(league2);
            Dictionary<string, int> league3Dict = GetBestDefenceTeamDict(league3);
            var league1MinValue = league1Dict.FirstOrDefault(x => x.Value == league1Dict.Values.Min());
            var league2MinValue = league2Dict.FirstOrDefault(x => x.Value == league2Dict.Values.Min());
            var league3MinValue = league3Dict.FirstOrDefault(x => x.Value == league3Dict.Values.Min());
            bestDefTeams.Add(new BestDefTeam { Team = league1MinValue.Key, Score = league1MinValue.Value });
            bestDefTeams.Add(new BestDefTeam { Team = league2MinValue.Key, Score = league2MinValue.Value });
            bestDefTeams.Add(new BestDefTeam { Team = league3MinValue.Key, Score = league3MinValue.Value });
            return bestDefTeams;
        }

        private Dictionary<string, ScoreDifferences> GetBestDifferenceDict(League league)
        {
            Dictionary<string, ScoreDifferences> temp = new Dictionary<string, ScoreDifferences>();
            foreach (var x in league.Matches)
            {
                if (x.Score != null)
                {
                    if (!temp.ContainsKey(x.Team1))
                    {
                        temp.Add(x.Team1, new ScoreDifferences() 
                        { 
                            Score = x.Score.ft[0], 
                            ScoreDiff = x.Score.ft[0] - x.Score.ft[1] 
                        });
                    }
                    else
                    {
                        temp[x.Team1].addScore(x.Score.ft[0]);
                        temp[x.Team1].addScoreDiff(x.Score.ft[0] - x.Score.ft[1]);
                        
                    }
                    if (!temp.ContainsKey(x.Team2))
                    {
                        temp.Add(x.Team2, new ScoreDifferences()
                        {
                            Score = x.Score.ft[1],
                            ScoreDiff = x.Score.ft[1] - x.Score.ft[0]
                        });
                    }
                    else
                    {
                        temp[x.Team2].addScore(x.Score.ft[1]);
                        temp[x.Team2].addScoreDiff(x.Score.ft[1] - x.Score.ft[0]);
                    }
                }

            }
            return temp;
        }
        public List<BestStatistic> GetBestStatisticTeams()
        {
            List<BestStatistic> bestStat = new List<BestStatistic>();
            League league1 = _repository.GetLeagues(en1);
            League league2 = _repository.GetLeagues(en2);
            League league3 = _repository.GetLeagues(en3);
            Dictionary<string, ScoreDifferences> league1Dict = GetBestDifferenceDict(league1);
            Dictionary<string, ScoreDifferences> league2Dict = GetBestDifferenceDict(league2);
            Dictionary<string, ScoreDifferences> league3Dict = GetBestDifferenceDict(league3);
            var league1BestStat = league1Dict.FirstOrDefault(x => x.Value.ScoreDiff == league1Dict.Values.Max(o => o.ScoreDiff) 
            && x.Value.Score == league1Dict.Values.Max(x => x.Score));
            var league2BestStat = league2Dict.FirstOrDefault(x => x.Value.ScoreDiff == league2Dict.Values.Max(o => o.ScoreDiff) 
            && x.Value.Score == league2Dict.Values.Max(x => x.Score));
            var league3BestStat = league3Dict.FirstOrDefault(x => x.Value.ScoreDiff == league3Dict.Values.Max(o => o.ScoreDiff) 
            && x.Value.Score == league3Dict.Values.Max(x => x.Score));
            bestStat.Add(new BestStatistic { 
                Name = league1BestStat.Key, 
                Scored = league1BestStat.Value.Score,
                Missed = league1BestStat.Value.Score - league1BestStat.Value.ScoreDiff   
            });
            bestStat.Add(new BestStatistic
            {
                Name = league2BestStat.Key,
                Scored = league2BestStat.Value.Score,
                Missed = league2BestStat.Value.Score - league2BestStat.Value.ScoreDiff
            });
            bestStat.Add(new BestStatistic
            {
                Name = league3BestStat.Key,
                Scored = league3BestStat.Value.Score,
                Missed = league3BestStat.Value.Score - league3BestStat.Value.ScoreDiff
            });
            return bestStat;
        }
        public MostResultDay GetMostResultDay()
        {
            List<MostResultDay> mostResultDays = new List<MostResultDay>();
            League league1 = _repository.GetLeagues(en1);
            League league2 = _repository.GetLeagues(en2);
            League league3 = _repository.GetLeagues(en3);
            Dictionary<string, int> league1Dict = GetDayDict(league1);
            Dictionary<string, int> league2Dict = GetDayDict(league2);
            Dictionary<string, int> league3Dict = GetDayDict(league3);
            var league1MostResultDay = league1Dict.FirstOrDefault(x => x.Value == league1Dict.Values.Max());
            var league2MostResultDay = league2Dict.FirstOrDefault(x => x.Value == league2Dict.Values.Max());
            var league3MostResultDay = league3Dict.FirstOrDefault(x => x.Value == league3Dict.Values.Max());
            mostResultDays.Add(new MostResultDay 
            { 
                DayName = league1MostResultDay.Key, 
                LeagueName = league1.Name, 
                Score = league1MostResultDay.Value 
            });
            mostResultDays.Add(new MostResultDay
            {
                DayName = league2MostResultDay.Key,
                LeagueName = league2.Name,
                Score = league2MostResultDay.Value
            });
            mostResultDays.Add(new MostResultDay
            {
                DayName = league2MostResultDay.Key,
                LeagueName = league2.Name,
                Score = league2MostResultDay.Value
            });
            var totalMostResultDay = mostResultDays.FirstOrDefault(x => x.Score == mostResultDays.Max(o => o.Score));
            MostResultDay mostResultDay = new MostResultDay
            {
                DayName = totalMostResultDay.DayName,
                Score = totalMostResultDay.Score,
                LeagueName = totalMostResultDay.LeagueName
            };
            return mostResultDay;
        }
    }
}
