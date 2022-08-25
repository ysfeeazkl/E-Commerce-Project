using E_Commerce.Entities.Dtos.AuthDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.AuthValidator
{
  

    public class CustomerLoginWithPhoneDtoValidator : AbstractValidator<CustomerLoginWithPhoneDto>
    {
        public CustomerLoginWithPhoneDtoValidator()
        {
            RuleFor(a => a.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez.");
            RuleFor(a => a.PhoneNumber).Length(5, 90).WithMessage("Telefon numarası minimum 5 maksimum 90 karakter olmalıdır.");
            RuleFor(a => a.Password).Length(8, 80).WithMessage("Şifre alanı minimum 8 maksimum 80 karakter olmalıdır.");
            RuleFor(x => x.Password)
       .NotEmpty().WithMessage("Parola alanı boş bırakılamaz.")
       .Matches(@"[][""!@$%&*(){}:;,.?/+_=\\-]")
        .WithMessage("Parola özel bir karakter içermelidir");
        }
    }
}
