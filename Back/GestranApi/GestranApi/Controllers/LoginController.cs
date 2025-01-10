using GestranApi.DTOs.Api;
using GestranApi.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GestranApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly JwtService _jwtService;

        public LoginController(ILogger<UsuarioController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var result = await _jwtService.Authneticate(request);
            if (result is null)
                return Unauthorized();

            return result;
        }

    }
}