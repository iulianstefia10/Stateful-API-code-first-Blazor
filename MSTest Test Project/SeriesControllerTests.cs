using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stateful_API_Code_first.Contrôleurs;
using Stateful_API_Code_first.Models.DTO;
using Stateful_API_Code_first.Models.Repository;
using APIfilms.Models.EntityFramework;
using Stateful_API_Code_first.Profiles;

namespace MSTest_Test_Project
{
    [TestClass]
    public class SeriesControllerTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            // Initialize a real AutoMapper instance using your profile for the tests
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new SeriesProfile()));
            _mapper = mapperConfig.CreateMapper();
        }

        [TestMethod]
        public async Task GetSeries_ReturnsOkResult_WithIEnumerableSerieDto()
        {
            var mockManager = new Mock<ISeriesManager>();
            var mockSeries = new List<Serie>
            {
                new Serie { SerieId = 1, Title = "Breaking Bad", CreationYear = 2008, Network = "AMC" },
                new Serie { SerieId = 2, Title = "The Office", CreationYear = 2005, Network = "NBC" }
            };

            mockManager.Setup(m => m.GetAllAsync()).ReturnsAsync(mockSeries);

            // 👉 Pass the _mapper to the controller
            var controller = new SeriesController(mockManager.Object, _mapper);

            var result = await controller.GetSeries();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnValue = okResult.Value as IEnumerable<SerieDto>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(2, returnValue.Count());
        }

        [TestMethod]
        public async Task GetSerie_ExistingId_ReturnsOkResult_WithSeriesDetailDto()
        {
            var mockManager = new Mock<ISeriesManager>();
            var expectedSerie = new Serie { SerieId = 1, Title = "Arcane", NbSaisons = 2, NbEpisodes = 18, CreationYear = 2024 };

            mockManager.Setup(m => m.GetByIdAsync(1)).ReturnsAsync(expectedSerie);

            // 👉 Pass the _mapper to the controller
            var controller = new SeriesController(mockManager.Object, _mapper);

            var result = await controller.GetSerie(1);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnValue = okResult.Value as SeriesDetailDto;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("Arcane", returnValue.Title);
            Assert.AreEqual(9.0, returnValue.AverageEpisodesPerSeason);
            Assert.IsTrue(returnValue.EstEnCours); 
        }

        [TestMethod]
        public async Task GetNetworks_ReturnsOkResult_WithAggregatedMetrics()
        {
            var mockManager = new Mock<ISeriesManager>();
            var expectedStats = new List<NetworkDto>
            {
                new NetworkDto { NetworkName = "HBO", NbSeries = 3, TotalEpisodes = 30 },
                new NetworkDto { NetworkName = "Netflix", NbSeries = 5, TotalEpisodes = 50 }
            };

            mockManager.Setup(m => m.GetNetworksStatsAsync()).ReturnsAsync(expectedStats);

            
            var controller = new SeriesController(mockManager.Object, _mapper);

            var result = await controller.GetNetworks();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnValue = okResult.Value as IEnumerable<NetworkDto>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(2, returnValue.Count());
        }
    }
}