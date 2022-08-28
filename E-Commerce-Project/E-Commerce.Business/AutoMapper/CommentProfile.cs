using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.CategoryDtos;
using E_Commerce.Entities.Dtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentAddDto, Category>();
            CreateMap<CommentUpdateDto, Category>();
        }
    }
}
