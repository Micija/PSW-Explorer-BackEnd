using PSW24.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.Public
{
    public interface IUserService
    {
        Result<UserDto> GetById(long userId);
        Result<List<UserDto>> GetSuspicious();
        Result<List<UserDto>> GetBlocked();
        Result<UserDto> Block(long userId);
        Result<UserDto> Unblock(long userId);
    }
}
