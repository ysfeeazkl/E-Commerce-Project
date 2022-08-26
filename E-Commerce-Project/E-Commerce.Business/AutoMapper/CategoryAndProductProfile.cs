using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.CategoryAndProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class CategoryAndProductProfile : Profile
    {
        public CategoryAndProductProfile()
        {
            CreateMap<CategoryAndProductAddDto, CategoryAndProduct>();
            CreateMap<CategoryAndProductUpdateDto, CategoryAndProduct>();
        }
    }
}
