using System;

namespace CrimeStatistics.Business.Models.PoliceApi
{
    public class CrimeStreetRequest
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime Month { get; set; } 
    }
}
