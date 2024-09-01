using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.Core.Services;

namespace PSW24_BackEnd.Controllers
{
    [Route("api/problems")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpPost]
        public ActionResult<ProblemDto> CreateProblem([FromBody] ProblemDto dto)
        {
            var result = _problemService.Create(dto);
            return CreateResponse(result);
        }
    }
}
