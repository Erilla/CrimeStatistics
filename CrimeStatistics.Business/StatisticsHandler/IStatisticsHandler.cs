using CrimeStatistics.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeStatistics.Business.StatisticsHandler
{
    public interface IStatisticsHandler
    {
        public Task<Statistics> GetCrimeStatisticsAsync();
    }
}
