using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.ProductPictureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class ProductPictureProfile : Profile
    {
        public ProductPictureProfile()
        {
            CreateMap<ProductPictureAddDto, ProductPicture>();
            CreateMap<ProductPictureUpdateDto, ProductPicture>();
        }
    }
}
