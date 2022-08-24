using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.UserTokenDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{

    public class UserTokenProfile : Profile
    {
        public UserTokenProfile()
        {
            CreateMap<UserTokenDto, UserToken>().ReverseMap();
        }

    }
}
