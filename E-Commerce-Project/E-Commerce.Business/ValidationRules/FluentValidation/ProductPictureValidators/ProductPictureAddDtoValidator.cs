using E_Commerce.Entities.Dtos.ProductPictureDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ProductPictureValidators
{


    public class ProductPictureAddDtoValidator : AbstractValidator<ProductPictureAddDto>
    {
        public ProductPictureAddDtoValidator()
        {
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.ProductId).NotEmpty();

        }
    }
}
