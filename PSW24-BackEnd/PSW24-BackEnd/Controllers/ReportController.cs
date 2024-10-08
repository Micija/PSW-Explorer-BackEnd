﻿using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.Core.Services;
namespace PSW24_BackEnd.Controllers
{
    [Route("api/reports")]

    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("for-author")]
        public ActionResult<List<ReportDto>> GetAllForAuthor()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _reportService.GetAllForAuthor(loggedUserId);
            return CreateResponse(result);
        }


    }
}
