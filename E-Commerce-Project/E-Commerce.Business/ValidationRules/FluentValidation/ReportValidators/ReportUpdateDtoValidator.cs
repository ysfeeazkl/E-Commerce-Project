using E_Commerce.Entities.Dtos.ReportDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ReportValidators
{
    public class ReportUpdateDtoValidator : AbstractValidator<ReportUpdateDto>
    {
        public ReportUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.Content).NotEmpty();         
        }
    }
}
