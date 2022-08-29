using E_Commerce.Entities.Dtos.SellerPictureDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.SellerPictureValidators
{
    public class SellerPictureUpdateDtoValidator : AbstractValidator<SellerPictureUpdateDto>
    {
        public SellerPictureUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.SellerID).NotEmpty();
        }
    }
}
