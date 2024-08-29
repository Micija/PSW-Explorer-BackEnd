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
    public class TourProfile : Profile
    {
        public TourProfile()
        {
            CreateMap<TourDto, Tour>().ReverseMap();
        }
    }
}
