using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Commands.Category;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.Interfaces;
using MediatR;

namespace Kaidao.Domain.CommandHandlers
{
    public class CategoryCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediatorHandler Bus;

        public CategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _categoryRepository = categoryRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewCategoryCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var category = new Category(Guid.NewGuid(), message.Name);

            if (_categoryRepository.GetByName(category.Name) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The category has already been taken."));
                return Task.FromResult(false);
            }

            _categoryRepository.Add(category);

            if (Commit())
            {
                //Bus.RaiseEvent(new AuthorRegisteredEvent(author.Id, author.Name));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _categoryRepository.Dispose();
        }
    }
}