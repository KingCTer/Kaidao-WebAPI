using System.Collections.Generic;

namespace Kaidao.Application.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}