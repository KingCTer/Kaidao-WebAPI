using Kaidao.Application.Common;
using Kaidao.Application.Responses;

namespace Kaidao.Web.Admin.ViewModels
{
    public class ChapterListViewModel
    {
        public PagedResult<ChapterResponse> ChapterPagedResult { get; set; }
        public string BookName { get; set; }
        public string BookId { get; set; }
    }
}
