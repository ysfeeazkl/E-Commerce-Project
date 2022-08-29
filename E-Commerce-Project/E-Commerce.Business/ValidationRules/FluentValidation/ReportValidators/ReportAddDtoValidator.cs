using E_Commerce.Entities.Dtos.ReportDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.ReportValidators
{

    public class ReportAddDtoValidator : AbstractValidator<ReportAddDto>
    {
        public ReportAddDtoValidator()
        {
            RuleFor(a => a.File).NotEmpty();
            RuleFor(a => a.CustomerID).NotEmpty();
            RuleFor(a => a.Content).NotEmpty();

            RuleFor(a => a.SellerID).GreaterThan(0).When(a => a.ProductID == 0 && a.BrandID == 0 && a.CommentID ==0);
            RuleFor(a => a.ProductID).GreaterThan(0).When(a => a.SellerID == 0 && a.BrandID == 0 && a.CommentID == 0);
            RuleFor(a => a.BrandID).GreaterThan(0).When(a => a.ProductID == 0 && a.SellerID == 0 && a.CommentID == 0);
            RuleFor(a => a.CommentID).GreaterThan(0).When(a => a.ProductID == 0 && a.SellerID == 0 && a.BrandID==0);
        }
    }
}
