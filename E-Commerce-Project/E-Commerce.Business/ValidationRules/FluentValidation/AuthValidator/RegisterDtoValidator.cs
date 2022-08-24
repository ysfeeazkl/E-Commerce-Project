using E_Commerce.Entities.Dtos.CustomerDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.AuthValidator
{
    public class RegisterDtoValidator : AbstractValidator<CustomerRegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(a => a.EmailAddress).Length(5, 80).WithMessage("Mail Adresi minimum 5 maksimum 80 karakter olmalıdır.");
            RuleFor(a => a.FirstName).Length(3, 50).WithMessage("İsim alanı minimum 3 maksimum 50 karakter olmalıdır.");
            RuleFor(a => a.LastName).Length(2, 50).WithMessage("Soyisim alanı minimum 3 maksimum 50 karakter olmalıdır.");
            RuleFor(a => a.Password).Length(8, 50).WithMessage("Şifre alanı minimum 8 maksimum 50 karakter olmalıdır.");
            RuleFor(a => a.PhoneNumber).MinimumLength(5).WithMessage("Telefon alanı minimum 5 karakter olmalıdır.");
            RuleFor(a => a.UserName).Length(3, 15).WithMessage("Kullanıcı Adı alanı minimum 3 maksimum 50 karakter olmalıdır.");
        }
    }
}
