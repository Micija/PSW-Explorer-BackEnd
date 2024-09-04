using PSW24.API.DTOs;
using PSW24.API.Public;
using PSW24.BuildingBlocks.Core.UseCases;
using PSW24.Core.Domain;
using PSW24.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;

namespace PSW24.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IInterestRepository _interestRepository;

        public AuthService(IUserRepository userRepository,ITokenGenerator tokenGenerator, IUserInterestRepository userInterestRepository, IInterestRepository interestRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _userInterestRepository = userInterestRepository;
            _interestRepository = interestRepository;   
        }

        public Result<AuthenticationTokensDto> Login(LoginDto credentials)
        {
            var user = _userRepository.GetActiveByName(credentials.Username);
            if (user == null || credentials.Password != user.Password) return Result.Fail(FailureCode.NotFound);

            return _tokenGenerator.GenerateAccessToken(user);
        }

        public Result<AuthenticationTokensDto> RegisterTourist(RegisterDto account)
        {
            if (_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

            try
            {
                var user = _userRepository.Create(new User(account.Username, account.Password, (UserRole)account.Role, true, account.Name, account.Surname, account.Email, 0, 0));

                foreach(var i in account.Interests)
                {
                    Interest interest = _interestRepository.GetByType(i);
                    if(interest == null) return Result.Fail(FailureCode.NotFound);
                    UserInterest userInterest = new(user.Id, interest.Id);
                    _userInterestRepository.Create(userInterest);
                    user.AddInterest(userInterest);
                }

                return _tokenGenerator.GenerateAccessToken(user);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
