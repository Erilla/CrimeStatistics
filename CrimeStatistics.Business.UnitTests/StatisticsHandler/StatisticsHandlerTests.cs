using CrimeStatistics.Business.Models.PoliceApi;
using CrimeStatistics.Business.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeStatistics.Business.UnitTests.StatisticsHandler
{
    [TestClass]
    public class StatisticsHandlerTests
    {
        private Business.StatisticsHandler.StatisticsHandler handler;

        private Mock<ICrimesRepository> crimeRepositoryMock;

        [TestInitialize]
        public void InitialiseTests()
        {
            crimeRepositoryMock = new Mock<ICrimesRepository>();
        }

        [TestMethod]
        public async Task GetCrimeStatistics_SuccessfulResponseAsync()
        {
            var crimeStreetResponse = new CrimeStreetResponse
            {
                CrimeStreets = new List<CrimeStreet>
                { 
                    new CrimeStreet
                    {
                        category = "category",
                        id = 123,
                        month = "month"
                    } 
                }
            };

            crimeRepositoryMock
                .Setup(cr => cr.GetCrimeStreet(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<DateTime>()))
                .ReturnsAsync(crimeStreetResponse);

            var crimeCategoriesResponse = new CrimeCategoriesResponse
            {
                CrimeCategories = new List<CrimeCategory>
                {
                    new CrimeCategory
                    {
                        name = "CategoryName",
                        url = "category"
                    }
                }
            };

            crimeRepositoryMock.Setup(cr => cr.GetCrimeCategories())
                .ReturnsAsync(crimeCategoriesResponse);

            handler = new Business.StatisticsHandler.StatisticsHandler(crimeRepositoryMock.Object);

            var result = await handler.GetCrimeStatisticsAsync(1, 2, DateTime.Now);

            Assert.IsNotNull(result);
        }
    }
}
