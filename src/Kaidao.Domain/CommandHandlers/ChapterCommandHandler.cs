using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Core.Notifications;
using Kaidao.Domain.Interfaces;
using MediatR;

namespace Kaidao.Domain.CommandHandlers
{
    public class ChapterCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewChapterCommand, bool>
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IMediatorHandler Bus;

        public ChapterCommandHandler(
            IChapterRepository chapterRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _chapterRepository = chapterRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewChapterCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var chapter = new Chapter(Guid.NewGuid(), message.Order, message.Name, message.Url, message.Content, message.BookId);

            if (_chapterRepository.GetChapterByBookIdAndOrder(chapter.BookId, chapter.Order) != null)
            {
                //Bus.RaiseEvent(new DomainNotification(message.MessageType, "The chapter has already been taken."));
                return Task.FromResult(false);
            }

            _chapterRepository.Add(chapter);

            if (Commit())
            {
                //Bus.RaiseEvent(new AuthorRegisteredEvent(author.Id, author.Name));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _chapterRepository.Dispose();
        }
    }
}