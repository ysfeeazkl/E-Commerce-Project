using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.SellerPictureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class SellerPictureProfile : Profile
    {
        public SellerPictureProfile()
        {
            CreateMap<SellerPictureAddDto, SellerPicture>();
            CreateMap<SellerPictureUpdateDto, SellerPicture>();
        }
    }
}
