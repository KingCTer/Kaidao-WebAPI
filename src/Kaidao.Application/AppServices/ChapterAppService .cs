using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Responses;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Interfaces;
using Kaidao.Domain.Specifications;
using Kaidao.Infra.Data.Repository.EventSourcing;

namespace Kaidao.Application.AppServices
{
    public class ChapterAppService : IChapterAppService
    {
        private readonly IMapper _mapper;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;
        private readonly IChapterRepository _chapterRepository;

        public ChapterAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IChapterRepository chapterRepository
            )
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _chapterRepository = chapterRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ChapterResponse GetChapterByBookIdAndOrder(Guid bookId, int order)
        {
            var chapter = _chapterRepository.GetChapterByBookIdAndOrder(bookId, order);

            return _mapper.Map<ChapterResponse>(chapter);
        }

        public ChapterResponse GetChapterById(Guid id)
        {
            var chapter = _chapterRepository.GetById(id);

            return _mapper.Map<ChapterResponse>(chapter);
        }

        public IEnumerable<ChapterResponse> GetChapterListByBookId(Guid bookId)
        {
            return _chapterRepository.GetChapterListByBookId(bookId).ProjectTo<ChapterResponse>(_mapper.ConfigurationProvider);
        }

        public RepositoryResponse<ChapterResponse> GetChapterListByBookId(Guid bookId, int skip, int take, string query)
        {
            var response = _chapterRepository.GetPagination(new ChapterFilterPaginatedSpecification(bookId, skip, take, query));
            var bookResponses = response.Queryable.ProjectTo<ChapterResponse>(_mapper.ConfigurationProvider);

            return new RepositoryResponse<ChapterResponse>(bookResponses, response.TotalRecords);
        }

        public ChapterResponse GetLastChapterByBookId(Guid bookId)
        {
            var chapter = _chapterRepository.GetLastChapterByBookId(bookId);

            return _mapper.Map<ChapterResponse>(chapter);
        }
    }
}