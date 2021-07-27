using CrimeStatistics.Business.Models.PoliceApi;
using System;
using System.Threading.Tasks;

namespace CrimeStatistics.Business.Repositories
{
    public interface ICrimesRepository
    {
        Task<CrimeStreetResponse> GetCrimeStreet(decimal latitude, decimal longitude, DateTime month);

        Task<CrimeCategoriesResponse> GetCrimeCategories();
    }
}
