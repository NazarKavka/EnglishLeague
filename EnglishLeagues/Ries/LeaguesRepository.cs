using EnglishLeagues.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishLeagues.Ries
{
    public class LeaguesRepository
    {
       public League GetLeagues(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<League>(jsonString);
        }
    }
}
