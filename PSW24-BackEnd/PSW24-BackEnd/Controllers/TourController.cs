using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.Core.Services;

namespace PSW24_BackEnd.Controllers
{
    [Route("api/tours")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<List<InterestDto>> GetAll()
        {
            var result = _tourService.GetAll();
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] TourDto dto)
        {
            var result = _tourService.Create(dto);
            return CreateResponse(result);
        }

        [HttpGet("for-user")]
        public ActionResult<List<InterestDto>> GetForUser()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _tourService.GetForUser(loggedUserId);
            return CreateResponse(result);
        }
    }
}
