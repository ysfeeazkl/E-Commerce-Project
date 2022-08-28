using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.AuthDtos;
using E_Commerce.Entities.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerLoginWithPhoneDto, Customer>();
            CreateMap<CustomerLoginWithEmailDto, Customer>();
            CreateMap<CustomerRegisterDto, Customer>();
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
        }

    }
}
