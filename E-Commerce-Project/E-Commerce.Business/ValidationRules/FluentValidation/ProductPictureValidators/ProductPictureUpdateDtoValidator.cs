using E_Commerce.Entities.Dtos.ProductPictureDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ProductPictureValidators
{

    public class ProductPictureUpdateDtoValidator : AbstractValidator<ProductPictureUpdateDto>
    {
        public ProductPictureUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty().GreaterThan(0);
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.ProductId).NotEmpty();

        }
    }
}
