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
        private readonly IUserRepository _userRepository;
        private readonly ITourRepository _tourRepository;

        public CartService(IMapper mapper, ICartRepository cartRepository, IUserRepository userRepository, ITourRepository tourRepository) : base(mapper)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _tourRepository = tourRepository;
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

        public Result<CartDto> Delete(long cartId)
        {
            Cart cart = _cartRepository.Get(cartId);
            if(cart  == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                _cartRepository.Delete(cart);
                return MapToDto(cart);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }
        



    }

}
