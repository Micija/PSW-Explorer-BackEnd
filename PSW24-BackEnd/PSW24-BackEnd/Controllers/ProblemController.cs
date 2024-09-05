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

        [HttpGet("for-author")]
        public ActionResult<List<ProblemDto>> GetForAuthor()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _problemService.GetForAuthor(loggedUserId);
            return CreateResponse(result);
        }
        [HttpGet("new-for-author")]
        public ActionResult<List<ProblemDto>> GetNewForAuthor()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _problemService.GetNewForAuthor(loggedUserId);
            return CreateResponse(result);
        }

        [HttpPatch("solve/{problemId}")]
        public ActionResult<ProblemDto> Solve(long problemId) {
            var result = _problemService.Solve(problemId);
            return CreateResponse(result);
        }

        [HttpPatch("revision/{problemId}")]
        public ActionResult<ProblemDto> Revision(long problemId)
        {
            var result = _problemService.Revision(problemId);
            return CreateResponse(result);
        }

        [HttpGet("all-admin-revision")]
        public ActionResult<List<ProblemDto>> GetRevisionForAdmin()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _problemService.GetRevisionForAdmin(loggedUserId);
            return CreateResponse(result);
        }

        [HttpPatch("on-hold/{problemId}")]
        public ActionResult<ProblemDto> OnHold(long problemId)
        {
            var result = _problemService.OnHold(problemId);
            return CreateResponse(result);
        }

        [HttpPatch("reject/{problemId}")]
        public ActionResult<ProblemDto> Reject(long problemId)
        {
            var result = _problemService.Reject(problemId);
            return CreateResponse(result);
        }

        [HttpGet("for-tourist")]
        public ActionResult<List<ProblemDto>> GetForTourist()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _problemService.GetForTourist(loggedUserId);
            return CreateResponse(result);
        }
    }
}
