using Kaidao.Domain.Validations.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaidao.Domain.Commands.Book
{
    public class RemoveBookCommand : BookCommand
    {

        public RemoveBookCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
