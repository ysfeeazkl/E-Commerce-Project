using E_Commerce.Entities.Dtos.CustomerPictureDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.CustomerPictureValidators
{
    public class CustomerPictureUpdateDtoValidator : AbstractValidator<CustomerPictureUpdateDto>
    {
        public CustomerPictureUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.CustomerId).NotEmpty();
        }
    }
}
