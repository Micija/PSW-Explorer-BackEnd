using AutoMapper;
using FluentResults;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.BuildingBlocks.Core.UseCases;
using PSW24.Core.Domain;
using PSW24.Core.Domain.Enums;
using PSW24.Core.Domain.RepositoryInterfaces;

namespace PSW24.Core.Services
{
    public class TourService : BaseService<TourDto, Tour>, ITourService
    {
        protected readonly ITourRepository _tourRepository;
        protected readonly IInterestRepository _interestRepository;
        protected readonly IUserRepository _userRepository;

        public TourService(ITourRepository tourRepository, IInterestRepository interestRepository, IUserRepository userRepository ,IMapper mapper) : base(mapper)
        {
            _tourRepository = tourRepository;
            _interestRepository = interestRepository;
            _userRepository = userRepository;
        }

        public Result<TourDto> Create(TourDto dto)
        {
            try
            {
                Tour tour = MapToDomain(dto);
                tour.Draft();
                tour = _tourRepository.Create(tour);

                return Result.Ok<TourDto>(MapToDto(tour));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<TourDto>> GetAll()
        {
            var result = _tourRepository.GetAll().ToList();
            return MapToDto(result);
        }

           

    }
}
