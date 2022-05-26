using FluentValidation;
using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Commands.Role;

namespace Kaidao.Domain.Validations.Role
{
    public abstract class RoleValidation<T> : AbstractValidator<T> where T : RoleCommand
    {
        protected void ValidateName()
        {
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(string.Empty);
        }
    }
}