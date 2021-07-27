using CrimeStatistics.Business.Models;
using CrimeStatistics.Business.StatisticsHandler;
using CrimeStatistics.Controllers;
using CrimeStatistics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CrimeStatistics.UnitTests.Controllers
{
    [TestClass]
    public class StatisticsControllerTests
    {
        private StatisticsController controller;

        private Mock<IStatisticsHandler> statisticsHandlerMock;

        [TestInitialize]
        public void InitialiseTest()
        {
            statisticsHandlerMock = new Mock<IStatisticsHandler>();
        }

        [TestMethod]
        public void Get_Index_ReturnsCorrectDataAsync()
        {
            controller = new StatisticsController(statisticsHandlerMock.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);

            var model = result.ViewData.Model as StatisticsViewModel;

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task Post_IndexAsync_ReturnsCorrectDataAsync()
        {
            statisticsHandlerMock
                .Setup(sh => sh.GetCrimeStatisticsAsync(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new Statistics());

            controller = new StatisticsController(statisticsHandlerMock.Object);

            var result = await controller.IndexAsync(new StatisticsViewModel()) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.ViewData.Model as StatisticsViewModel;

            Assert.IsNotNull(model);
        }
    }
}
