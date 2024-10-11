using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
