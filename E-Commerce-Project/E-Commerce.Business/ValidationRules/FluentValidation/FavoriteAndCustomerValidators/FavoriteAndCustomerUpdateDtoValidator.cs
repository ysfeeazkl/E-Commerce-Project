using E_Commerce.Entities.Dtos.FavoriteAndCustomerDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.FavoriteAndCustomerValidators
{
    public class FavoriteAndCustomerUpdateDtoValidator : AbstractValidator<FavoriteAndCustomerUpdateDto>
    {
        public FavoriteAndCustomerUpdateDtoValidator()
        {
            RuleFor(a => a.CustomerID).NotEmpty().GreaterThan(0).WithMessage("Customer alanı boş geçilmez");
            RuleFor(a => a.ProductID).NotEmpty().GreaterThan(0).WithMessage("Product alanı boş geçilmez");

            RuleFor(a => a.NewProductID).GreaterThan(0).When(a => a.NewProductID != null);
            RuleFor(a => a.NewCustomerID).GreaterThan(0).When(a => a.NewCustomerID != null);

        }
    }
}
