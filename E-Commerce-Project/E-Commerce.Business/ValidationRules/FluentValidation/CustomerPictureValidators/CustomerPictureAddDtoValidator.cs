using E_Commerce.Entities.Dtos.CustomerPictureDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.CustomerPictureValidators
{
    public class CustomerPictureAddDtoValidator : AbstractValidator<CustomerPictureAddDto>
    {
        public CustomerPictureAddDtoValidator()
        {
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.CustomerId).NotEmpty();

        }
    }
}
