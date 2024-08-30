using FluentResults;
using PSW24.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.Public
{
    public interface ICartService
    {
        Result<CartDto> Create(CartDto dto);
        Result<CartDto> Delete(long cartId);
    }
}
