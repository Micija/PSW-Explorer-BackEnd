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
    public class CartService : BaseService<CartDto, Cart>, ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(IMapper mapper, ICartRepository userRepository) : base(mapper)
        {
            _cartRepository = userRepository;
        }

        public Result<CartDto> Create(CartDto dto)
        {
            Cart cart = MapToDomain(dto);
            try
            {
                _cartRepository.Create(cart);
                return MapToDto(cart);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }
    }
}
