using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.OData.Query;

namespace API.Controllers {
    [Route("odata/[controller]")]
    [ApiController]
    public class NewArticlesController : ControllerBase {

        private readonly INewArticlesService newArticlesService;

        public NewArticlesController(INewArticlesService newArticlesService) {
            this.newArticlesService = newArticlesService;
        }


        [EnableQuery]
        [Authorize(Roles = "99")]
        [HttpGet]
        public IActionResult GetAllNews() {
            var result = newArticlesService.GetAll();
            return Ok(result);

        }
    }
}
