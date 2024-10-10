using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service;


namespace API.Controllers {
    
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request) {
            var member = _authService.Login(request.Email, request.Password);
            if (member == null) return Unauthorized();

            return Ok(new { Token = member });
        }
    }

    public class LoginRequest {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
