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

        public async Task<Statistics> GetCrimeStatisticsAsync(decimal latitude, decimal longitude, DateTime month)
        {
            var crimeStreetResponse = await _crimesRepository.GetCrimeStreet(latitude, longitude, month);
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
