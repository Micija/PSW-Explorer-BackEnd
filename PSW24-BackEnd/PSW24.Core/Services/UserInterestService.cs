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
    public class UserInterestService : CrudService<UserInterestDto, UserInterest>, IUserInterestService
    {
        private readonly IUserInterestRepository _userInterestRepository;

        public UserInterestService(ICrudRepository<UserInterest> crudRepository, IMapper mapper, IUserInterestRepository userRepository) : base(crudRepository, mapper)
        {
            _userInterestRepository = userRepository;
        }
    }
}
