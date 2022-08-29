using AutoMapper;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.ReportPictureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AutoMapper
{
    public class ReportPictureProfile : Profile
    {
        public ReportPictureProfile()
        {
            CreateMap<ReportPictureAddDto, ReportPicture>();
            CreateMap<ReportPictureUpdateDto, ReportPicture>();
        }
    }
}
