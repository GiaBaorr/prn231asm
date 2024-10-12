using API.DTO;
using Data.Models;
using Presentation;

namespace Service {

    public interface INewArticlesService {

        public List<NewsArticle> GetAll();
        public NewsArticle GetById(String id);
        NewsArticle Create(ArticlesDTO articleDto);
        NewsArticle? Update(string id, ArticlesDTO articleDto);
        NewsArticle? Delete(string id);
    }

    public class NewArticlesService : INewArticlesService {
        private INewsArticleRepo newsArticleRepo;

        public NewArticlesService(INewsArticleRepo newsArticleRepo) {
            this.newsArticleRepo = newsArticleRepo;
        }

        public List<NewsArticle> GetAll() {
            return newsArticleRepo.GetAll();
        }

        public NewsArticle GetById(String id) {
            return newsArticleRepo.GetById(id);
        }

        public NewsArticle Create(ArticlesDTO articleDto) {
            var newArticle = new NewsArticle {
                NewsArticleId = Guid.NewGuid().ToString(),
                NewsTitle = articleDto.NewsTitle,
                Headline = articleDto.Headline,
                CreatedDate = DateTime.Now,
                NewsContent = articleDto.NewsContent,
                NewsSource = articleDto.NewsSource,
                CategoryId = articleDto.CategoryId,
                NewsStatus = articleDto.NewsStatus,
                CreatedById = articleDto.CreatedById,
                ModifiedDate = DateTime.Now,
                UpdatedById = articleDto.UpdatedById,
            };

            return newsArticleRepo.Create(newArticle);
        }

        public NewsArticle? Update(string id, ArticlesDTO articleDto) {
            var existingArticle = newsArticleRepo.GetById(id);
            if (existingArticle == null) return null;

            existingArticle.NewsTitle = articleDto.NewsTitle;
            existingArticle.Headline = articleDto.Headline;
            existingArticle.NewsContent = articleDto.NewsContent;
            existingArticle.NewsSource = articleDto.NewsSource;
            existingArticle.CategoryId = articleDto.CategoryId;
            existingArticle.NewsStatus = articleDto.NewsStatus;
            existingArticle.UpdatedById = articleDto.UpdatedById;
            existingArticle.ModifiedDate = DateTime.Now;

            return newsArticleRepo.Update(existingArticle);
        }

        public NewsArticle? Delete(string id) {
            var existingArticle = newsArticleRepo.GetById(id);
            if (existingArticle == null) return null;

            return newsArticleRepo.Delete(existingArticle);
        }
    }
}
