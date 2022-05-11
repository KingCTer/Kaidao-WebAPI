using FluentValidation.Results;
using Kaidao.Domain.Core.Commands;
using Kaidao.Domain.Core.Events;

namespace Kaidao.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task<ValidationResult> SendCommandWithValidationResult<T>(T command) where T : Command;

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}