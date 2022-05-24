using FluentValidation.Results;
using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;

namespace Kaidao.Application.AppServices.Interfaces
{
    public interface IBookAppService : IDisposable
    {
        public void Crawl(string url);

        public BookResponse GetById(Guid id);

        public IEnumerable<BookResponse> GetAll();

        public RepositoryResponse<BookResponse> GetAll(int skip, int take, string query);

        bool Create(BookCreateRequest bookCreateRequest);

        bool Update(BookUpdateRequest bookUpdateRequest);

        bool Remove(Guid id);
    }
}