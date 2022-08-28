using E_Commerce.Entities.Dtos.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ProductValidators
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.Name).NotEmpty().WithMessage("İsim alanı boş geçilmez");
            RuleFor(a => a.Name).Length(1, 200).WithMessage("Geçerli bir ürün isimi giriniz");
            RuleFor(a => a.Description).NotEmpty().WithMessage("Geçerli açıklama giriniz");
            RuleFor(a => a.SellerID).NotEmpty();
        }
    }
}
