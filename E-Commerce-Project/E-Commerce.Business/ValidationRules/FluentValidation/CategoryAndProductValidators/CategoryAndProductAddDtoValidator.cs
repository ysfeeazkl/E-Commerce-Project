using E_Commerce.Entities.Dtos.CategoryAndProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.CategoryAndProductValidator
{
    public class CategoryAndProductAddDtoValidator : AbstractValidator<CategoryAndProductAddDto>
    {
        public CategoryAndProductAddDtoValidator()
        {
            RuleFor(a => a.CategoryID).GreaterThan(0).WithMessage("Category alanı boş geçilmez");
            RuleFor(a => a.ProductID).GreaterThan(0).WithMessage("Product alanı boş geçilmez");
      
        }
    }
}
