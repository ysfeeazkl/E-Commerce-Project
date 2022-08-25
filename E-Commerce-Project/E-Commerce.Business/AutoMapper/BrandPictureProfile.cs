using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.BrandPictureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class BrandPictureProfile : Profile
    {
        public BrandPictureProfile()
        {
            CreateMap<BrandPictureAddDto, Brand>();
            CreateMap<BrandPictureUpdateDto, Brand>();
        }
    }
}
