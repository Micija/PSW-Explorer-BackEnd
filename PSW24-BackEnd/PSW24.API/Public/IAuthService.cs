﻿using PSW24.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.Public
{
    public interface IAuthService
    {
        Result<AuthenticationTokensDto> Login(LoginDto credentials);
        Result<AuthenticationTokensDto> RegisterTourist(RegisterDto account);
    }
}
