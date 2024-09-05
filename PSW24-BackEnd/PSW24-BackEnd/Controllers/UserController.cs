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

        [HttpGet("suspicious-users")]
        public ActionResult<List<UserDto>> GetSuspicious()
        {
            var result = _userService.GetSuspicious();
            return CreateResponse(result);  
        }

        [HttpPatch("block/{userId}")]
        public ActionResult<List<UserDto>> Block(long userId)
        {
            var result = _userService.Block(userId);
            return CreateResponse(result);
        }

        [HttpGet("blocked-users")]
        public ActionResult<List<UserDto>> GetBlocked()
        {
            var result = _userService.GetBlocked();
            return CreateResponse(result);
        }

        [HttpPatch("unblock/{userId}")]
        public ActionResult<List<UserDto>> Unblock(long userId)
        {
            var result = _userService.Unblock(userId);
            return CreateResponse(result);
        }

        [HttpPatch("change-interest")]
        public ActionResult<UserDto> ChangeInterest([FromBody] List<String> interests)
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _userService.ChangeInterest(loggedUserId, interests);
            return CreateResponse(result);
        }

    }
}
