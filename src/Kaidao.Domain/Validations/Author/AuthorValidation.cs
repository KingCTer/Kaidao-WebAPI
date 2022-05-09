﻿using FluentValidation;
using Kaidao.Domain.Commands.Author;

namespace Kaidao.Domain.Validations.Author
{
    public abstract class AuthorValidation<T> : AbstractValidator<T> where T : AuthorCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}