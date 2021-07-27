using CrimeStatistics.Business.Models;
using CrimeStatistics.Business.Models.PoliceApi;
using CrimeStatistics.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var crimeStreetResponse = await _crimesRepository.GetCrimeStreet(51.44237m, -2.49810m, new DateTime(2021, 01, 01));
            var crimeCategoryResponse = await _crimesRepository.GetCrimeCategories();

            return new Statistics { Crimes = GenerateCrimes(crimeStreetResponse, crimeCategoryResponse) };
        }

        private static List<Crime> GenerateCrimes(CrimeStreetResponse crimeStreetResponse, CrimeCategoriesResponse crimeCategoryResponse)
        {
            var crimes = new List<Crime>();

            foreach (var crimeStreet in crimeStreetResponse.CrimeStreets)
            {
                crimes.Add(new Crime
                {
                    CategoryName = crimeCategoryResponse.CrimeCategories.First(cc => cc.url == crimeStreet.category).name
                });
            }

            return crimes.OrderBy(c => c.CategoryName).ToList();
        }
    }
}
