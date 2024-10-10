using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Presentation {
    public interface INewsArticleRepo {
        public Task<List<NewsArticle>> GetAll();
    }

    public class NewArticlesRepoRepository :INewsArticleRepo {
        
        private readonly FUNewsManagementDbContext _context;

        public NewArticlesRepoRepository(FUNewsManagementDbContext context) {
            _context = context;
        }

        public async Task<List<NewsArticle>> GetAll() {
            return await _context.NewsArticles.ToListAsync();
        }



    }
}
