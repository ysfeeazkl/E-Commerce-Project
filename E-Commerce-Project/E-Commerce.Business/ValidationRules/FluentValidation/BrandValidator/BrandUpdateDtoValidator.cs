using E_Commerce.Entities.Dtos.BrandDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.BrandValidator
{
    public class BrandUpdateDtoValidator : AbstractValidator<BrandUpdateDto>
    {
        public BrandUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.Name).NotEmpty().WithMessage("İsim alanı boş geçilmez");
            RuleFor(a => a.Name).Length(1, 100).WithMessage("Geçerli bir Marka isimi giriniz");
            RuleFor(a => a.Introduc).NotEmpty().WithMessage("Açıklama alanı boş geçilmez");
            RuleFor(a => a.Introduc).Length(5, 400).WithMessage("Geçerli bir tanıdım giriniz");
        }
    }
}
