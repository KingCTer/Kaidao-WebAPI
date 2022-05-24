using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;

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

        bool Create(ChapterViewModel chapterViewModel);

        bool Update(ChapterViewModel chapterViewModel);

        bool Remove(Guid chapterId);
    }
}