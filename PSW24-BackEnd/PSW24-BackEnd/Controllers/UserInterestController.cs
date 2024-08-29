using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.Core.Domain;

namespace PSW24_BackEnd.Controllers
{
    [Route("api/user-interest")]
    public class UserInterestController : BaseApiController
    {
        private readonly IUserInterestService _userInterestService;

        public UserInterestController(IUserInterestService userInterestService)
        {
            _userInterestService = userInterestService;
        }


    }
}
