﻿using AutoMapper;
using FluentResults;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.BuildingBlocks.Core.UseCases;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Services
{
    public class KeyPointService : BaseService<KeyPointDto, KeyPoint>, IKeyPointService
    {
        protected readonly IKeyPointRepository _keyPointRepository;

        public KeyPointService(IKeyPointRepository keyPointRepository, IMapper mapper) : base(mapper)
        {
            _keyPointRepository = keyPointRepository;
        }

        public Result<KeyPointDto> Create(KeyPointDto dto)
        {
            try
            {
                KeyPoint keyPoint = MapToDomain(dto);
                keyPoint = _keyPointRepository.Create(keyPoint);

                return Result.Ok<KeyPointDto>(MapToDto(keyPoint));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<KeyPointDto>> GetAllForTour(long tourId)
        {
            try
            {
                var keyPoints = _keyPointRepository.GetAll().FindAll(k => k.TourId == tourId);
                var keyPointDtos = keyPoints.Select(k => new KeyPointDto
                {
                    Id = k.Id,
                    Description = k.Description,
                    Latitude = k.Latitude,
                    Longitude = k.Longitude,
                    Image = k.Image,
                    Name = k.Name,
                    TourId = k.TourId,

                }).ToList();

                return Result.Ok(keyPointDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<KeyPointDto>>("Failed to retrieve menus").WithError(e.Message);
            }
        }
    }
}
