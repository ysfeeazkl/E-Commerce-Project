using E_Commerce.Entities.Dtos.CommentDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.ValidationRules.FluentValidation.CommentValidators
{
    public class CommentUpdateDtoValidator : AbstractValidator<CommentUpdateDto>
    {
        public CommentUpdateDtoValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
            RuleFor(a => a.Content).NotEmpty().WithMessage("İçerik alanı boş geçilmez");
            RuleFor(a => a.CustomerID).NotEmpty().GreaterThan(0);

        }
    }
}
