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
        private readonly IMapper _mapper;
        protected readonly IInterestRepository _interestRepository;

        public InterestService(IInterestRepository interestRepository, IMapper mapper) : base(mapper)
        {
            _interestRepository = interestRepository;
            _mapper = mapper;
        }

    }
}