using BytPax.Models;
using BytPax.Repositories;
using System.Collections.Generic;
using System.Linq;
using BytPax.Instructions;
using BytPax.Services.Extra;

namespace BytPax.Services
{
    public class SearchService
    {
        private readonly IDataStorage<Athlete> _athleteRepo;
        private readonly IDataStorage<Article> _articleRepo;
        private readonly IDataStorage<RecordHistory> _recordRepo;
        private readonly IDataStorage<HistoricalEvent> _eventRepo;

        public SearchService(IDataStorage<Athlete> athleteRepo, 
            IDataStorage<Article> articleRepo,
            IDataStorage<RecordHistory> recordRepo,
            IDataStorage<HistoricalEvent> eventRepo)
        {
            _athleteRepo = athleteRepo;
            _articleRepo = articleRepo;
            _recordRepo = recordRepo;
            _eventRepo = eventRepo;
        }

        public DashboardData GetDashboardData()
        {
            var athletes = _athleteRepo.GetAll();
            var articles = _articleRepo.GetAll();
            var records = _recordRepo.GetAll();
            var events = _eventRepo.GetAll();

            return new DashboardData
            {
                AthletesCount = athletes.Count(),
                ArticlesCount = articles.Count(),
                RecordsCount = records.Count(),
                EventsCount = events.Count(),
                Athletes = athletes,
                Articles = articles,
                Records = records,
                Events = events
            };
        }

        public List<Athlete> SearchAthletes(string searchTerm)
        {
            return _athleteRepo.GetAll()
                .Where(a => string.IsNullOrEmpty(searchTerm) || a.FullName.Contains(searchTerm))
                .ToList();
        }

        public List<HistoricalEvent> SearchEvents(string searchTerm)
        {
            return _eventRepo.GetAll()
                .Where(e => string.IsNullOrEmpty(searchTerm) || e.Title.Contains(searchTerm))
                .ToList();
        }

        public List<RecordHistory> SearchRecords(string searchTerm)
        {
            return _recordRepo.GetAll()
                .Where(r => string.IsNullOrEmpty(searchTerm) || r.AthleteName.Contains(searchTerm))
                .ToList();
        }

        public List<Article> SearchArticles(string searchTerm)
        {
            return _articleRepo.GetAll()
                .Where(a => string.IsNullOrEmpty(searchTerm) || a.Topic.Contains(searchTerm))
                .ToList();
        }
    }
}
