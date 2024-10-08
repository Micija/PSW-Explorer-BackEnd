﻿using FluentResults;
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
        public ActionResult<TourDto> CreateTour([FromBody] TourDto dto)
        {
            var result = _tourService.Create(dto);
            return CreateResponse(result);
        }

        [HttpGet("for-user")]
        public ActionResult<List<TourDto>> GetForUser()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _tourService.GetForUser(loggedUserId);
            return CreateResponse(result);
        }

        [HttpPatch("set-publish/{tourId}")]
        public ActionResult<TourDto> Publish(long tourId)
        {
            var result = _tourService.Publish(tourId);
            return CreateResponse(result);
        }

        [HttpGet("get-publish")]
        public ActionResult<List<InterestDto>> GetPublish()
        {
            var result = _tourService.GetPublish();
            return CreateResponse(result);
        }

        [HttpGet("get-author")]
        public ActionResult<List<TourDto>> GetAuthor()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _tourService.GetAuthor(loggedUserId);
            return CreateResponse(result);
        }

        [HttpPatch("set-archive/{tourId}")]
        public ActionResult<TourDto> Archive(long tourId)
        {
            var result = _tourService.Archive(tourId);
            return CreateResponse(result);
        }


        [HttpGet("in-cart")]
        public ActionResult<List<TourDto>> GetCartTour()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _tourService.GetCartTour(loggedUserId);
            return CreateResponse(result);
        }


        [HttpGet("{id}")]
        public ActionResult<TourDto> GetTourById(long id)
        {
            var tourDto = _tourService.GetTourById(id);
            if (tourDto == null)
            {
                return NotFound(); // Return 404 if the tour is not found
            }
            return Ok(tourDto); // Return 200 with the tour DTO if found
        }


        [HttpGet("recommendations/{difficulty}")]
        public ActionResult<TourDto> GetRecommendations(string difficulty)
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var tourDto = _tourService.GetRecommendations(loggedUserId, difficulty);
            return CreateResponse(tourDto); 
        }

        [HttpGet("awarded")]
        public ActionResult<List<TourDto>> GetAwarder()
        {
            var tourDto = _tourService.GetAwarder();
            return CreateResponse(tourDto);
        }

        [HttpGet("bought")]
        public ActionResult<List<TourDto>> GetBoughtTours()
        {
            var loggedUserId = long.Parse(User.FindFirst("id")?.Value);
            var result = _tourService.GetBoughtTours(loggedUserId);
            return CreateResponse(result);
        }
    }
}
