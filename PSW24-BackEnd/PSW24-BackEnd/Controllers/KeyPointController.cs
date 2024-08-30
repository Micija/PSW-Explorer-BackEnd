using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.Core.Domain;
using PSW24.Core.Services;

namespace PSW24_BackEnd.Controllers
{
    [Route("api/keyPoints")]
    public class KeyPointController : BaseApiController
    {
        
        private readonly IKeyPointService _keyPointService;

        public KeyPointController(IKeyPointService keyPointService)
        {
            _keyPointService = keyPointService;
        }


        [HttpPost]
        public ActionResult<KeyPointDto> Create([FromBody] KeyPointDto dto)
        {
            var result = _keyPointService.Create(dto);
            return CreateResponse(result);
        }

    }
}
