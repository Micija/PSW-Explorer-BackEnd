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
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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

        public Result<bool> Buy(long customerId)
        {
            User user = _userRepository.GetById(customerId);
            if (user == null) return Result.Fail(FailureCode.NotFound);
            try
            {
                string boughts = "";
                foreach(var cart in _cartRepository.GetCustomer(user))
                {
                    cart.Buy();
                    boughts += cart.Tour.Name + ",";
                    _cartRepository.Save();
                }


                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("markoandjelicpsw@gmail.com", "yasg svva qiep ddfo"),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("markoandjelicpsw@gmail.com"),
                    To = { user.Email },
                    Subject = "Kupljene ture",
                    Body = "Hvala na kupovini: " + boughts,
                    IsBodyHtml = true
                };

                try
                {
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to send email: {ex.Message}");
                }
                finally
                {
                    mailMessage.Dispose();
                    client.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

    }

}
