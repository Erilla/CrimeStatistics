using CrimeStatistics.Business.Models;
using CrimeStatistics.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeStatistics.Business.StatisticsHandler
{
    public class StatisticsHandler : IStatisticsHandler
    {
        private readonly ICrimesRepository _crimesRepository;

        public StatisticsHandler(ICrimesRepository crimeRepository)
        {
            _crimesRepository = crimeRepository;
        }

        public async Task<Statistics> GetCrimeStatisticsAsync()
        {
            var categories = await _crimesRepository.GetCrimeCategories();
            return new Statistics { Categories = categories.CrimeCategories[0].name };
        }
    }
}
