﻿using FluentResults;
using PSW24.API.DTOs;
using PSW24.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.Public
{
    public interface IInterestService 
    {
        Result<List<InterestDto>> GetAll();
        Result<List<InterestDto>> GetForUser(long userId);
    }
}
