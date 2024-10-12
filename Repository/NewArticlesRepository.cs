using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Presentation {
    public interface INewsArticleRepo {
        public List<NewsArticle> GetAll();
        NewsArticle GetById(string id);
        NewsArticle Create(NewsArticle article);
        NewsArticle Update(NewsArticle article);
        NewsArticle? Delete(NewsArticle article);
    }

    public class NewArticlesRepoRepository : INewsArticleRepo {

        private readonly FUNewsManagementDbContext _context;

        public NewArticlesRepoRepository(FUNewsManagementDbContext context) {
            _context = context;
        }

        public List<NewsArticle> GetAll() {
            return _context.NewsArticles.ToList();
        }

        public NewsArticle GetById(String id) {
            return _context.NewsArticles.FirstOrDefault(x => x.NewsArticleId == id);
        }
        public NewsArticle Create(NewsArticle article) {
            _context.NewsArticles.Add(article);
            _context.SaveChanges();
            return article;
        }

        public NewsArticle Update(NewsArticle article) {
            _context.NewsArticles.Update(article);
            _context.SaveChanges();
            return article;
        }

        public NewsArticle? Delete(NewsArticle article) {
            _context.NewsArticles.Remove(article);
            _context.SaveChanges();
            return article;
        }
    }
}
