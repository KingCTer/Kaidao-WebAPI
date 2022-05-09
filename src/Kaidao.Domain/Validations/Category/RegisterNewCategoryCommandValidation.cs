using Kaidao.Domain.Commands.Category;

namespace Kaidao.Domain.Validations.Category
{
    public class RegisterNewCategoryCommandValidation : CategoryValidation<RegisterNewCategoryCommand>
    {
        public RegisterNewCategoryCommandValidation()
        {
            ValidateName();
        }
    }
}