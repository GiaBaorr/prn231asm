using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Presentation {
    public interface INewsArticleRepo {
        public List<NewsArticle> GetAll();
    }

    public class NewArticlesRepoRepository :INewsArticleRepo {
        
        private readonly FUNewsManagementDbContext _context;

        public NewArticlesRepoRepository(FUNewsManagementDbContext context) {
            _context = context;
        }

        public List<NewsArticle> GetAll() {
            return _context.NewsArticles.ToList();
        }



    }
}
