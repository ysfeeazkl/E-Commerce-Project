using E_Commerce.Entities.Dtos.BrandPictureDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.BrandPictureValidator
{
    public class BrandPictureUpdateDtoValidator : AbstractValidator<BrandPictureUpdateDto>
    {
        public BrandPictureUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.BrandId).NotEmpty();

        }
    }
}
