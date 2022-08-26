using E_Commerce.Entities.Dtos.CategoryAndProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.CategoryAndProductValidators
{

    public class CategoryAndProductUpdateDtoValidator : AbstractValidator<CategoryAndProductUpdateDto>
    {
        public CategoryAndProductUpdateDtoValidator()
        {
            RuleFor(a => a.CategoryID).NotEmpty().GreaterThan(0).WithMessage("Category alanı boş geçilmez");
            RuleFor(a => a.ProductID).NotEmpty().GreaterThan(0).WithMessage("Product alanı boş geçilmez");

            RuleFor(a => a.NewProductID).GreaterThan(0).When(a=>a.NewProductID!=null);
            RuleFor(a => a.NewCategoryID).GreaterThan(0).When(a=>a.NewCategoryID != null);

        }
    }
}
