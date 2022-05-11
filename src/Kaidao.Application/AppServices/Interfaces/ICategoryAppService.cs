using Kaidao.Application.Responses;

namespace Kaidao.Application.AppServices.Interfaces
{
    public interface ICategoryAppService : IDisposable
    {
        public IEnumerable<CategoryResponse> GetAll();

    }
}