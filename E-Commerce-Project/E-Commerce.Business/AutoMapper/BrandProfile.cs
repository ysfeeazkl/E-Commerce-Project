using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.BrandDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandAddDto, Brand>();
            CreateMap<BrandUpdateDto, Brand>();
        }
    }
}
