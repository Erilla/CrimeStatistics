using CrimeStatistics.Business.Models;
using System.Collections.Generic;

namespace CrimeStatistics.Models
{
    public class StatisticsViewModel
    {
        public StatisticsViewModel()
        {
            Error = new List<string>();
        }

        public decimal? Latitude { get; set; }
        
        public decimal? Longitude { get; set; }

        public string Month { get; set; }

        public List<string> Error { get; set; }

        public Statistics Statistics { get; set; }

        public List<string> Categories { get; set; }
    }
}
