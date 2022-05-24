using Kaidao.Application.Responses;

namespace Kaidao.Web.Admin.ViewModels
{
    public class ChapterCreateViewModel
    {
        public BookResponse Book { get; set; }

        public int Order { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
    }
}
