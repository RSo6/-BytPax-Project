using BytPax.Models;
using BytPax.Repositories;
using BytPax.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BytPax.Instructions;

namespace BytPax.Tests.Services
{
    [TestFixture]
    public class SearchServiceTests
    {
        private Mock<IDataStorage<Athlete>> _mockAthleteRepo = null!;
        private Mock<IDataStorage<Article>> _mockArticleRepo = null!;
        private Mock<IDataStorage<RecordHistory>> _mockRecordRepo = null!;
        private Mock<IDataStorage<HistoricalEvent>> _mockEventRepo = null!;
        private SearchService _searchService = null!;

        [SetUp]
        public void Setup()
        {
            _mockAthleteRepo = new Mock<IDataStorage<Athlete>>();
            _mockArticleRepo = new Mock<IDataStorage<Article>>();
            _mockRecordRepo = new Mock<IDataStorage<RecordHistory>>();
            _mockEventRepo = new Mock<IDataStorage<HistoricalEvent>>();

            _searchService = new SearchService(
                _mockAthleteRepo.Object,
                _mockArticleRepo.Object,
                _mockRecordRepo.Object,
                _mockEventRepo.Object);
        }

        [Test]
        public void GetDashboardData_ReturnsCorrectCountsAndCollections()
        {
            // Arrange
            var athletes = new List<Athlete> { new Athlete { FullName = "John Doe" } };
            var articles = new List<Article> { new Article { Topic = "Sports" } };
            var records = new List<RecordHistory> { new RecordHistory { AthleteName = "John Doe" } };
            var events = new List<HistoricalEvent> { new HistoricalEvent { Title = "Olympics" } };

            _mockAthleteRepo.Setup(r => r.GetAll()).Returns(athletes);
            _mockArticleRepo.Setup(r => r.GetAll()).Returns(articles);
            _mockRecordRepo.Setup(r => r.GetAll()).Returns(records);
            _mockEventRepo.Setup(r => r.GetAll()).Returns(events);

            // Act
            dynamic result = _searchService.GetDashboardData();

            // Assert
            Assert.AreEqual(1, result.AthletesCount);
            Assert.AreEqual(1, result.ArticlesCount);
            Assert.AreEqual(1, result.RecordsCount);
            Assert.AreEqual(1, result.EventsCount);
            Assert.AreEqual(athletes, result.Athletes);
            Assert.AreEqual(articles, result.Articles);
            Assert.AreEqual(records, result.Records);
            Assert.AreEqual(events, result.Events);
        }

        [Test]
        public void SearchAthletes_ReturnsAll_WhenSearchTermIsEmpty()
        {
            // Arrange
            var athletes = new List<Athlete>
            {
                new Athlete { FullName = "John Doe" },
                new Athlete { FullName = "Jane Smith" }
            };
            _mockAthleteRepo.Setup(r => r.GetAll()).Returns(athletes);

            // Act
            var result = _searchService.SearchAthletes("");

            // Assert
            Assert.AreEqual(2, result.Count);
            CollectionAssert.AreEquivalent(athletes, result);
        }

        [Test]
        public void SearchAthletes_FiltersByFullName()
        {
            // Arrange
            var athletes = new List<Athlete>
            {
                new Athlete { FullName = "John Doe" },
                new Athlete { FullName = "Jane Smith" }
            };
            _mockAthleteRepo.Setup(r => r.GetAll()).Returns(athletes);

            // Act
            var result = _searchService.SearchAthletes("Jane");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Jane Smith", result[0].FullName);
        }
        
    }
}
