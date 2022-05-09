using Kaidao.Application.Responses;

namespace Kaidao.Application.AppServices.Interfaces
{
    public interface IBookAppService : IDisposable
    {
        public void Crawl(string url);

        public BookResponse GetById(Guid id);

        public IEnumerable<BookResponse> GetAll();

        public RepositoryResponse<BookResponse> GetAll(int skip, int take, string query);
    }
}