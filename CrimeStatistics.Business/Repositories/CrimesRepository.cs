using CrimeStatistics.Business.Models.PoliceApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrimeStatistics.Business.Repositories
{
    public class CrimesRepository : ICrimesRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public CrimesRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CrimeCategoriesResponse> GetCrimeCategories()
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://data.police.uk/api/crime-categories"); // TODO: Add url into config

            using var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                var categoriesList = await JsonSerializer.DeserializeAsync<List<CrimeCategory>>(json);
                return new CrimeCategoriesResponse { CrimeCategories = categoriesList };
            } 
            else
            {
                throw new HttpRequestException("Request GetCrimeCategories failed", null, response.StatusCode);
            }
        }

        public Task<CrimeStreetResponse> GetCrimeStreet(string latitude, string longitude, DateTime month)
        {
            throw new NotImplementedException();
        }
    }
}
