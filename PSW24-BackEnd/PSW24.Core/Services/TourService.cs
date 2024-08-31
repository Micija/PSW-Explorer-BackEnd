using AutoMapper;
using FluentResults;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.BuildingBlocks.Core.UseCases;
using PSW24.Core.Domain;
using PSW24.Core.Domain.Enums;
using PSW24.Core.Domain.RepositoryInterfaces;
using System.Linq;

namespace PSW24.Core.Services
{
    public class TourService : BaseService<TourDto, Tour>, ITourService
    {
        protected readonly ITourRepository _tourRepository;
        protected readonly IInterestRepository _interestRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly ICartRepository _cartRepository;

        public TourService(ITourRepository tourRepository, IInterestRepository interestRepository, IUserRepository userRepository, ICartRepository cartRepository , IMapper mapper) : base(mapper)
        {
            _tourRepository = tourRepository;
            _interestRepository = interestRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
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

        private bool InInterest(List<Domain.UserInterest> interests, long interestId)
        {
            return interests.Any(i => i.InterestId == interestId);
        }
        public Result<List<TourDto>> GetForUser(long loggedUserId)
        {
            User user = _userRepository.GetById(loggedUserId);
            List<Tour> suitableTours = new();

            foreach (var tour in _tourRepository.GetAll().ToList())
            {
                if (InInterest(user.Interests, tour.InterestId)) suitableTours.Add(tour);
            }

            return MapToDto(suitableTours);
        }

        public Result<TourDto> Publish(long tourId)
        {
            Tour tour = _tourRepository.Get(tourId);
            if (tour == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                tour.Publish();
                _tourRepository.Save();
                return MapToDto(tour);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<TourDto>> GetPublish()
        {
            var result = _tourRepository.GetAll().FindAll(t => t.Status == TourStatus.PUBLISHED).ToList();
            return MapToDto(result);
        }

        public Result<List<TourDto>> GetAuthor(long authorId)
        {
            var result = _tourRepository.GetAll().FindAll(t => t.AuthorId == authorId).ToList();
            return MapToDto(result);
        }

        public Result<TourDto> Archive(long tourId)
        {
            Tour tour = _tourRepository.Get(tourId);
            if (tour == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                tour.Archive();
                _tourRepository.Save();
                return MapToDto(tour);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<TourDto>> GetCartTour(long customerId)
        {
            User user = _userRepository.GetById(customerId);
            if (user == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                List<Tour> suitableTours = new();

                foreach (var tourId in _cartRepository.GetCartCustomer(user))
                {
                    suitableTours.Add(_tourRepository.Get(tourId));
                }

                return MapToDto(suitableTours);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public TourDto GetTourById(long id)
        {
            var tour = _tourRepository.Get(id);

            if (tour == null)
            {
                return null; // Or handle appropriately if null
            }

            return MapToDto(tour);
        }

    }
}
