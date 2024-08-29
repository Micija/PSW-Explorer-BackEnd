using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;

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
    }
}
