using Kaidao.Application.Responses;

namespace Kaidao.Application.AppServices.Interfaces
{
    public interface IChapterAppService : IDisposable
    {
        public ChapterResponse GetChapterById(Guid id);

        public ChapterResponse GetChapterByBookIdAndOrder(Guid bookId, int order);

        public ChapterResponse GetLastChapterByBookId(Guid bookId);

        public IEnumerable<ChapterResponse> GetChapterListByBookId(Guid bookId);

        public RepositoryResponse<ChapterResponse> GetChapterListByBookId(Guid bookId, int skip, int take, string query);

        public string CrawlContent(ChapterResponse chapterResponse);
    }
}