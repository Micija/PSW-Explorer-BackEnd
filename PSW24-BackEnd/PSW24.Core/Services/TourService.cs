using AutoMapper;
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
    public class TourService : BaseService<TourDto, Tour>, ITourService
    {
        protected readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository, IMapper mapper) : base(mapper)
        {
            _tourRepository = tourRepository;
        }

        public Result<List<TourDto>> GetAll()
        {
            var result = _tourRepository.GetAll().ToList();
            return MapToDto(result);
        }
    }
}
