using E_Commerce.Entities.Dtos.ShoppingCartDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ShoppingCartValidators
{

    public class ShoppingCartUpdateDtoValidator : AbstractValidator<ShoppingCartUpdateDto>
    {
        public ShoppingCartUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.ProductID).NotEmpty();

        }
    }
}
