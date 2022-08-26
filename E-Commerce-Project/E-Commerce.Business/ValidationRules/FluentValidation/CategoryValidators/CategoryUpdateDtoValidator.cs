using E_Commerce.Entities.Dtos.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.CategoryValidators
{

    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.Name).NotEmpty().WithMessage("İsim alanı boş geçilmez");
            RuleFor(a => a.Description).NotEmpty().WithMessage("Açıklama alanı boş geçilmez");
        }
    }
}
