using CrimeStatistics.Business.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CrimeStatistics.Business.UnitTests.Repositories
{
    [TestClass]
    public class CrimeRepositoryTests
    {
        private CrimesRepository repository;

        private Mock<IHttpClientFactory> httpClientFactoryMock;

        [TestInitialize]
        public void InitialiseTests()
        {
            httpClientFactoryMock = new Mock<IHttpClientFactory>();
        }

        [TestMethod]
        public async Task GetCrimeCategories_SuccessResponseAsync()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{}]"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            repository = new CrimesRepository(httpClientFactoryMock.Object);

            var result = await repository.GetCrimeCategories();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetCrimeCategories_ErrorResponseAsync()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("[{}]"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            repository = new CrimesRepository(httpClientFactoryMock.Object);

            var result = await repository.GetCrimeCategories();
        }

        [TestMethod]
        public async Task GetCrimeStreet_SuccessResponseAsync()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{}]"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            repository = new CrimesRepository(httpClientFactoryMock.Object);

            var result = await repository.GetCrimeStreet(1, 2, DateTime.Now);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetCrimeStreet_ErrorResponseAsync()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("[{}]"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            repository = new CrimesRepository(httpClientFactoryMock.Object);

            var result = await repository.GetCrimeStreet(1, 2, DateTime.Now);
        }
    }
}
