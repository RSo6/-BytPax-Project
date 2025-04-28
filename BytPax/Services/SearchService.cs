using BytPax.Models;
using BytPax.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BytPax.Services
{
    public class SearchService
    {
        private readonly Repository<Athlete> _athleteRepo;
        private readonly Repository<Article> _articleRepo;
        private readonly Repository<RecordHistory> _recordRepo;
        private readonly Repository<HistoricalEvent> _eventRepo;

        public SearchService(Repository<Athlete> athleteRepo, 
                           Repository<Article> articleRepo,
                           Repository<RecordHistory> recordRepo,
                           Repository<HistoricalEvent> eventRepo)
        {
            _athleteRepo = athleteRepo;
            _articleRepo = articleRepo;
            _recordRepo = recordRepo;
            _eventRepo = eventRepo;
        }

        public dynamic GetDashboardData()
        {
            var athletes = _athleteRepo.GetAll();
            var articles = _articleRepo.GetAll();
            var records = _recordRepo.GetAll();
            var events = _eventRepo.GetAll();

            return new
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
