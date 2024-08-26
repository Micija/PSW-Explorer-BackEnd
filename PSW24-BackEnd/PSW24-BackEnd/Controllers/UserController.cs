using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PSW24_BackEnd.Controllers
{
    [Route("api/users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetById/{userId:int}")]
        public ActionResult<UserDto> GetById(long userId)
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _userService.GetById(userId);
            return CreateResponse(result);
        }

    }
}
