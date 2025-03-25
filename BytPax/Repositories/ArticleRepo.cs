using BytPax.Models;

namespace BytPax.Repositories;

public class ArticleRepo<T> : Repository<T> where T : Article
{
    public IEnumerable<T> GetArticlesSortedById()
    {
        return _entities.OrderBy(a => a.Id).ToList();
    }

    public IEnumerable<T> GetArticlesSortedByIdDescending()
    {
        return _entities.OrderByDescending(a => a.Id).ToList();
    }
}