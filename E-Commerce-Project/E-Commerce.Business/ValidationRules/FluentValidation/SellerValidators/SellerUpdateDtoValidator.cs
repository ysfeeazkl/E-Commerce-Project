using E_Commerce.Entities.Dtos.SellerDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.SellerValidators
{
    public class SellerUpdateDtoValidator : AbstractValidator<SellerUpdateDto>
    {
        public SellerUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.Name).NotEmpty();
        }
    }
}
