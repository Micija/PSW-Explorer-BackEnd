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
        protected readonly IProblemLoggerRepository _problemLoggerRepository;

        public ProblemService(IProblemRepository problemRepository, IInterestRepository interestRepository, IProblemLoggerRepository problemLoggerRepository , IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper) : base(mapper)
        {
            _problemRepository = problemRepository;
            _interestRepository = interestRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _problemLoggerRepository = problemLoggerRepository;
        }

        public Result<ProblemDto> Create(ProblemDto dto)
        {
            try
            {
                Problem problem = MapToDomain(dto);
                problem = _problemRepository.Create(problem);

                ProblemLogger problemLogger = new(dto.Id, problem.Status, null);
                _problemLoggerRepository.Create(problemLogger);

                return Result.Ok<ProblemDto>(MapToDto(problem));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<ProblemDto>> GetForAuthor(long authorId)
        {
            try
            {
                return MapToDto(_problemRepository.GetForAuthor(authorId));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<ProblemDto>> GetForTourist(long touristId)
        {
            try
            {
                return MapToDto(_problemRepository.GetForTourist(touristId));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<ProblemDto>> GetRevisionForAdmin(long authorId)
        {
            try
            {
                return MapToDto(_problemRepository.GetRevisionForAdmin(authorId));
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<ProblemDto> OnHold(long problemId)
        {
            Problem problem = _problemRepository.GetById(problemId);
            if (problem == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                ProblemLogger problemLogger = new(problem.Id, problem.Status, Domain.Enums.ProblemStatus.SOLVED);
                _problemLoggerRepository.Create(problemLogger);

                problem.OnHold();
                _problemRepository.Save();

                return MapToDto(problem);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<ProblemDto> Reject(long problemId)
        {
            Problem problem = _problemRepository.GetById(problemId);
            if (problem == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                ProblemLogger problemLogger = new(problem.Id, problem.Status, Domain.Enums.ProblemStatus.SOLVED);
                _problemLoggerRepository.Create(problemLogger);

                problem.Reject();
                _problemRepository.Save();

                return MapToDto(problem);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<ProblemDto> Revision(long problemId)
        {
            Problem problem = _problemRepository.GetById(problemId);
            if (problem == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                ProblemLogger problemLogger = new(problem.Id, problem.Status, Domain.Enums.ProblemStatus.SOLVED);
                _problemLoggerRepository.Create(problemLogger);

                problem.Revision();
                _problemRepository.Save();

                return MapToDto(problem);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<ProblemDto> Solve(long problemId)
        {
            Problem problem = _problemRepository.GetById(problemId);
            if (problem == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                ProblemLogger problemLogger = new(problem.Id, problem.Status, Domain.Enums.ProblemStatus.SOLVED);
                _problemLoggerRepository.Create(problemLogger);

                problem.Solve();
                _problemRepository.Save();

                return MapToDto(problem);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }
    }
}
