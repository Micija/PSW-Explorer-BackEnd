using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace PSW24_BackEnd.Controllers
{
    [Route("api/users")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] RegisterDto account)
        {
            var result = _authService.RegisterTourist(account);
            return CreateResponse(result);
        }

        [HttpPost("login")]
        public ActionResult<AuthenticationTokensDto> Login([FromBody] LoginDto credentials)
        {
            var result = _authService.Login(credentials);
            return CreateResponse(result);
        }


    }
}
