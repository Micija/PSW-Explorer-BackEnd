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
    public class InterestService : BaseService<InterestDto, Interest>, IInterestService
    {
        protected readonly IInterestRepository _interestRepository;
        protected readonly IUserInterestRepository _userInterestRepository;

        public InterestService(IInterestRepository interestRepository, IMapper mapper, IUserInterestRepository userInterestRepository) : base(mapper)
        {
            _interestRepository = interestRepository;
            _userInterestRepository = userInterestRepository;
        }

        public Result<List<InterestDto>> GetAll() {
            var result = _interestRepository.GetAll().ToList();
            return MapToDto(result);
        }

        public Result<List<InterestDto>> GetForUser(long userId)
        {
            List<Interest> interests = new();

            foreach(var ui in _userInterestRepository.GetForUser(userId))
            {
                interests.Add(_interestRepository.Get(ui.InterestId));
            }

            return MapToDto(interests);
        }

    }
}