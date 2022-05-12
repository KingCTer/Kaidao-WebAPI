using Kaidao.Application.Common;
using Kaidao.Application.Responses;

namespace Kaidao.Web.Portal.ViewModels
{
    public class BookDetailViewModel
    {
        public BookResponse CurrentBook { get; set; }
        public RepositoryResponse<BookResponse> CategoryBooks { get; set; }
        public RepositoryResponse<BookResponse> AuthorBooks { get; set; }
        public PagedResult<ChapterResponse> ChapterPagedResult { get; set; }
    }
}
