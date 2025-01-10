using GestranApi.DTOs.Api;
using GestranApi.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GestranApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticaApiController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly JwtService _jwtService;

        public AutenticaApiController(ILogger<UsuarioController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("LoginApi")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
        {
            var result = await _jwtService.Authneticate(request);
            if (result is null)
                return Unauthorized();

            return result;
        }

    }
}