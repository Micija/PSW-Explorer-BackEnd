using AutoMapper;
using PSW24.API.DTOs;
using PSW24.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.Core.Mappers
{
    internal class InterestProfile : Profile
    {
        public InterestProfile()
        {
            CreateMap<InterestDto, Interest>().ReverseMap();
        }
    }
}
