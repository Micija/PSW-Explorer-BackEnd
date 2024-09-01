using AutoMapper;
using FluentResults;
using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.BuildingBlocks.Core.UseCases;
using PSW24.Core.Domain.RepositoryInterfaces;
using PSW24.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Services
{
    public class ProblemService : BaseService<ProblemDto, Problem>, IProblemService
    {
        protected readonly IProblemRepository _problemRepository;
        protected readonly IInterestRepository _interestRepository;
        protected readonly IUserRepository _userRepository;
        protected readonly ICartRepository _cartRepository;

        public ProblemService(IProblemRepository problemRepository, IInterestRepository interestRepository, IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper) : base(mapper)
        {
            _problemRepository = problemRepository;
            _interestRepository = interestRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public Result<ProblemDto> Create(ProblemDto dto)
        {
            try
            {
                Problem problem = MapToDomain(dto);
                problem = _problemRepository.Create(problem);

                return Result.Ok<ProblemDto>(MapToDto(problem));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

    }
}
