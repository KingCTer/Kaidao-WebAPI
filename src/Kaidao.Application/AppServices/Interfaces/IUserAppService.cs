using Kaidao.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaidao.Application.AppServices.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        IEnumerable<UserResponse> GetAll();
        RepositoryResponse<UserResponse> GetAll(int skip, int take, string query);
    }
}
