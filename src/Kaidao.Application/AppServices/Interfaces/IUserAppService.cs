using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaidao.Application.AppServices.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        IEnumerable<UserViewModel> GetAll();
        RepositoryResponse<UserViewModel> GetAll(int skip, int take, string query);
    }
}
