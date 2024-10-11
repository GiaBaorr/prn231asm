using Data.Models;
using Presentation;

namespace Service {

    public interface INewArticlesService {

        public List<NewsArticle> GetAll();
    }
    
    public class NewArticlesService : INewArticlesService {
        private INewsArticleRepo newsArticleRepo;

        public NewArticlesService(INewsArticleRepo newsArticleRepo) {
            this.newsArticleRepo = newsArticleRepo;
        }

        public List<NewsArticle> GetAll() {
            return newsArticleRepo.GetAll();
        }
    }
}
