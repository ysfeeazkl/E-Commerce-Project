using E_Commerce.Entities.Dtos.ShoppingCartDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ShoppingCartValidators
{
    public class ShoppingCartAddDtoValidator : AbstractValidator<ShoppingCartAddDto>
    {
        public ShoppingCartAddDtoValidator()
        {
            RuleFor(a => a.CustomerID).NotEmpty();

        }
    }
}
