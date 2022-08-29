using E_Commerce.Entities.Dtos.ReportPictureDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ReportPictureValidators
{

    public class ReportPictureUpdateDtoValidator : AbstractValidator<ReportPictureUpdateDto>
    {
        public ReportPictureUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.ReportID).NotEmpty();
        }
    }
}
