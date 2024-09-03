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
        private readonly IUserRepository UserRepository;

        public UserService(ICrudRepository<User> crudRepository, IMapper mapper, IUserRepository userRepository) : base(crudRepository,mapper)
        {
            UserRepository = userRepository;
        }
        public Result<UserDto> GetById(long userId)
        {
            User user = UserRepository.GetById(userId);
            return MapToDto(user);
        }

        public Result<List<UserDto>> GetSuspicious() { 
            return MapToDto( UserRepository.GetSuspicious());
        }
        public Result<List<UserDto>> GetBlocked()
        {
            return MapToDto(UserRepository.GetBlocked());
        }

        public Result<UserDto> Block(long userId)
        {
            User user = UserRepository.GetById(userId);
            if(user == null) return Result.Fail(FailureCode.NotFound);
            user.Block();
            UserRepository.Save();
            return MapToDto(user);
        }

        public Result<UserDto> Unblock(long userId)
        {
            User user = UserRepository.GetById(userId);
            if (user == null) return Result.Fail(FailureCode.NotFound);
            user.Unblock();
            UserRepository.Save();
            return MapToDto(user);
        }
    }
}
