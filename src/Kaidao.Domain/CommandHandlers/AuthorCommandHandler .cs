using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Commands.Author;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.Interfaces;
using MediatR;

namespace Kaidao.Domain.CommandHandlers
{
    public class AuthorCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewAuthorCommand, bool>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMediatorHandler Bus;

        public AuthorCommandHandler(
            IAuthorRepository authorRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _authorRepository = authorRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewAuthorCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var author = new Author(Guid.NewGuid(), message.Name);

            if (_authorRepository.GetByName(author.Name) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The author has already been taken."));
                return Task.FromResult(false);
            }

            _authorRepository.Add(author);

            if (Commit())
            {
                //Bus.RaiseEvent(new AuthorRegisteredEvent(author.Id, author.Name));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _authorRepository.Dispose();
        }
    }
}