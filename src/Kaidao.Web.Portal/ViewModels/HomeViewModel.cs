using Kaidao.Application.Responses;

namespace Kaidao.Web.Portal.ViewModels
{
    public class HomeViewModel
    {
        public RepositoryResponse<BookResponse> NewlyBooks { get; set; }

        public RepositoryResponse<BookResponse> FavoriteBooks { get; set; }

        public RepositoryResponse<BookResponse> PopularBook { get; set; }
    }
}
