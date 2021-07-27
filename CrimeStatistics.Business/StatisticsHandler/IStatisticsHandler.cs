using CrimeStatistics.Business.Models;
using System;
using System.Threading.Tasks;

namespace CrimeStatistics.Business.StatisticsHandler
{
    public interface IStatisticsHandler
    {
        public Task<Statistics> GetCrimeStatisticsAsync(decimal latitude, decimal longitude, DateTime month);
    }
}
