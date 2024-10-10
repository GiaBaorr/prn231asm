using Data.Models;
using Presentation;

namespace Service {

    public interface INewArticlesService {

        public Task<List<NewsArticle>> GetAll();
    }
    
    public class NewArticlesService : INewArticlesService {
        private INewsArticleRepo newsArticleRepo;

        public NewArticlesService(INewsArticleRepo newsArticleRepo) {
            this.newsArticleRepo = newsArticleRepo;
        }

        public Task<List<NewsArticle>> GetAll() {
            return newsArticleRepo.GetAll();
        }
    }
}
