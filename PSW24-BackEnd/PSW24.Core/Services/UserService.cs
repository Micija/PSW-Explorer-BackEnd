using AutoMapper;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.BuildingBlocks.Core.UseCases;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Services
{
    public class UserService : CrudService<UserDto, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IInterestRepository _interestRepository;

        public UserService(ICrudRepository<User> crudRepository, IMapper mapper, IUserRepository userRepository, IUserInterestRepository userInterestRepository, IInterestRepository interestRepository) : base(crudRepository, mapper)
        {
            _userRepository = userRepository;
            _userInterestRepository = userInterestRepository;
            _interestRepository = interestRepository;   
        }
        public Result<UserDto> GetById(long userId)
        {
            User user = _userRepository.GetById(userId);
            return MapToDto(user);
        }

        public Result<List<UserDto>> GetSuspicious() { 
            return MapToDto(_userRepository.GetSuspicious());
        }
        public Result<List<UserDto>> GetBlocked()
        {
            return MapToDto(_userRepository.GetBlocked());
        }

        public Result<UserDto> Block(long userId)
        {
            User user = _userRepository.GetById(userId);
            if(user == null) return Result.Fail(FailureCode.NotFound);
            user.Block();
            _userRepository.Save();
            return MapToDto(user);
        }

        public Result<UserDto> Unblock(long userId)
        { 
            User user = _userRepository.GetById(userId);
            if (user == null) return Result.Fail(FailureCode.NotFound);
            user.Unblock();
            _userRepository.Save();
            return MapToDto(user);
        }

        public Result<UserDto> ChangeInterest(long userId, List<string> interests)
        {
            User user = _userRepository.GetById(userId);
            foreach(UserInterest ui in user.Interests)
            {
                _userInterestRepository.Delete(user.Id, ui.Id);
            }
            if (user == null) return Result.Fail(FailureCode.NotFound);
            foreach(string interest in interests) { 
                Interest interest1 = _interestRepository.GetByType(interest);
                UserInterest userInterest = new(user.Id, interest1.Id);
                _userInterestRepository.Create(userInterest);
            }
            return MapToDto(user);
        }
    }
}
