using API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Service;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Controllers {
    [ApiController]
    [Route("odata/[controller]")]
    public class NewsArticleController : ODataController {

        private readonly INewArticlesService _newArticlesService;

        public NewsArticleController(INewArticlesService newArticlesService) {
            this._newArticlesService = newArticlesService;
        }


        [EnableQuery]
        // [Authorize(Roles = "99")]
        [HttpGet]
        public IActionResult GetAllNews() {
            var result = _newArticlesService.GetAll();
            return Ok(result);

        }

        [EnableQuery]
        [HttpGet("{key}")]
        public IActionResult GetNewsById([FromODataUri] String key) {
            var result = _newArticlesService.GetById(key);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateNewsArticle([FromBody] ArticlesDTO articleDto) {
            if (articleDto == null) {
                return BadRequest("Article data is missing.");
            }

            var createdArticle = _newArticlesService.Create(articleDto);
            return Created(createdArticle.NewsArticleId, createdArticle);
        }

        [HttpPut("{key}")]
        public IActionResult UpdateNewsArticle([FromODataUri] string key, [FromBody] ArticlesDTO articleDto) {
            if (articleDto == null) {
                return BadRequest("Article data is missing.");
            }

            var updatedArticle = _newArticlesService.Update(key, articleDto);
            if (updatedArticle == null) {
                return NotFound();
            }

            return Updated(updatedArticle);
        }

        [HttpDelete("{key}")]
        public IActionResult DeleteNewsArticle([FromODataUri] string key) {
            var deletedArticle = _newArticlesService.Delete(key);
            if (deletedArticle == null) {
                return NotFound();
            }

            return NoContent();
        }

    }
}
