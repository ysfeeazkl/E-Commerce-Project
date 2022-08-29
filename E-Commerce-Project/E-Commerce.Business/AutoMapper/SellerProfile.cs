using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.SellerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<SellerAddDto, Seller>();
            CreateMap<SellerUpdateDto, Seller>();
        }
    }
}
