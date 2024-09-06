using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;

namespace PSW24_BackEnd.Controllers
{
    [Route("api/interest")]
    public class InterestController : BaseApiController
    {
        private readonly IInterestService _interestService;

        public InterestController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpGet]
        public ActionResult<List<InterestDto>> GetAll()
        {
            var result = _interestService.GetAll();
            return CreateResponse(result);
        }

        [HttpGet("for-user")]
        public ActionResult<List<InterestDto>> GetForUser()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _interestService.GetForUser(loggedUserId);
            return CreateResponse(result);
        }

    }
}