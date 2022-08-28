using E_Commerce.Entities.Dtos.FavoriteAndCustomerDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.FavoriteAndCustomerValidators
{
    public class FavoriteAndCustomerAddDtoValidator : AbstractValidator<FavoriteAndCustomerAddDto>
    {
        public FavoriteAndCustomerAddDtoValidator()
        {
            RuleFor(a => a.CustomerID).GreaterThan(0).WithMessage("Customer alanı boş geçilmez");
            RuleFor(a => a.ProductID).GreaterThan(0).WithMessage("Product alanı boş geçilmez");

        }
    }
}
