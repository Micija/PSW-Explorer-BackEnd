using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
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

    }
}