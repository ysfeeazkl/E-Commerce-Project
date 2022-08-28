using E_Commerce.Entities.Dtos.CommentDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.CommentValidators
{
    public class CommentAddDtoValidator : AbstractValidator<CommentAddDto>
    {
        public CommentAddDtoValidator()
        {
            RuleFor(a => a.Content).NotEmpty().WithMessage("İçerik alanı boş geçilmez");
            RuleFor(a => a.CustomerID).NotEmpty().GreaterThan(0);

            RuleFor(a => a.SellerID).GreaterThan(0).When(a=>a.ProductID==0 || a.BaseCommentID ==0);
            RuleFor(a => a.ProductID).GreaterThan(0).When(a => a.SellerID == 0 || a.BaseCommentID == 0);
            RuleFor(a => a.BaseCommentID).GreaterThan(0).When(a => a.ProductID == 0 || a.SellerID == 0);

        }
    }
}
