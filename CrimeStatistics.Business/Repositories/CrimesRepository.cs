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
                var stream = await response.Content.ReadAsStreamAsync();
                var categoriesList = await JsonSerializer.DeserializeAsync<List<CrimeCategory>>(stream);
                return new CrimeCategoriesResponse { CrimeCategories = categoriesList };
            } 
            else
            {
                throw new HttpRequestException("Request GetCrimeCategories failed", null, response.StatusCode);
            }
        }

        public async Task<CrimeStreetResponse> GetCrimeStreet(decimal latitude, decimal longitude, DateTime month)
        {
            var client = _clientFactory.CreateClient();

            var url = $"https://data.police.uk/api/crimes-street/all-crime?lat={latitude}&lng={longitude}&date={month:yyyy-MM}"; // TODO: Add url into config

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var crimeStreetsList = await JsonSerializer.DeserializeAsync<List<CrimeStreet>>(stream);
                return new CrimeStreetResponse { CrimeStreets = crimeStreetsList };
            }
            else
            {
                throw new HttpRequestException("Request GetCrimeStreet failed", null, response.StatusCode);
            }
        }
    }
}
